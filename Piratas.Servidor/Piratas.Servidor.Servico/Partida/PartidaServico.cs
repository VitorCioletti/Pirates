namespace Piratas.Servidor.Servico
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Cartas;
    using Dominio.Excecoes;
    using Excecoes;
    using Protocolo.Cliente;
    using Protocolo.Servidor;

    // TODO: Talvez configurar injeção de dependência?
    public class PartidaServico
    {
        public Guid IdMesa { get; private set; }

        private Mesa _mesa { get; set; }

        private Dictionary<Jogador, List<Acao>> _possiveisAcoesEnviadasAosJogadores { get; set; }

        private Dictionary<Guid, List<Evento>> _eventosAcaoAtual { get; set; }

        private object _lockObject { get; set; }

        public PartidaServico(List<Guid> idsJogadores)
        {
            var jogadores = new List<Jogador>();

            foreach (Guid idJogador in idsJogadores)
            {
                var jogador = new Jogador(
                    idJogador,
                    _aoAdicionarCartaNaMao,
                    _aoRemoverCartaNaMao,
                    _aoAdicionarCartaNoCampo,
                    _aoRemoverCartaNoCampo);

                jogadores.Add(jogador);
            }

            _lockObject = new Object();

            _mesa = new Mesa(jogadores);

            IdMesa = _mesa.Id;

            _possiveisAcoesEnviadasAosJogadores = new Dictionary<Jogador, List<Acao>>();
        }

        public List<MensagemServidor> ProcessarMensagemCliente(MensagemCliente mensagemCliente)
        {
            lock (_lockObject)
                return _processarMensagemCliente(mensagemCliente);
        }


        private List<MensagemServidor> _processarMensagemCliente(MensagemCliente mensagemCliente)
        {
            List<MensagemServidor> mensagensServidor = new List<MensagemServidor>();

            try
            {
                Jogador jogadorComAcaoPendente = _obterJogadorComAcaoPendente(mensagemCliente);
                Acao acaoPendente = _obterAcaoPendente(jogadorComAcaoPendente, mensagemCliente);

                Dictionary<Jogador, List<Acao>> acoesPosProcessamentoAcao = _mesa.ProcessarAcao(acaoPendente);

                foreach ((Jogador jogador, List<Acao> acoesDisponiveis) in acoesPosProcessamentoAcao)
                {
                    MensagemServidor mensagemServidor = _criarMensagemServidor(jogador, acoesDisponiveis);

                    mensagensServidor.Add(mensagemServidor);

                    _eventosAcaoAtual.Clear();
                    _possiveisAcoesEnviadasAosJogadores.Add(jogador, acoesDisponiveis);
                }

                _possiveisAcoesEnviadasAosJogadores[jogadorComAcaoPendente].Remove(acaoPendente);
            }
            catch (BaseServicoException servicoException)
            {
                mensagensServidor.Add(new MensagemServidor(servicoException.Id));
            }
            catch (BaseRegraException regraException)
            {
                mensagensServidor.Add(new MensagemServidor(regraException.Id));
            }

            return mensagensServidor;
        }

        private Acao _obterAcaoPendente(Jogador jogador, MensagemCliente mensagemCliente)
        {
            string idAcaoExecutada = mensagemCliente.IdAcaoExecutada;

            List<Acao> acoesPendentes = _possiveisAcoesEnviadasAosJogadores[jogador];

            Acao acaoPendente = acoesPendentes.FirstOrDefault(a => a.Id == idAcaoExecutada);

            if (acaoPendente == null)
                throw new AcaoNaoDisponivelException(idAcaoExecutada);

            return acaoPendente;
        }

        private Jogador _obterJogadorComAcaoPendente(MensagemCliente mensagemCliente)
        {
            Guid idJogadorRealizador = mensagemCliente.IdJogadorRealizador;

            (Jogador jogadorComAcaoPendente, List<Acao> _) =
                _possiveisAcoesEnviadasAosJogadores.FirstOrDefault(a => a.Key.Id == idJogadorRealizador);

            if (jogadorComAcaoPendente == null)
                throw new JogadorSemAcaoPendenteException(idJogadorRealizador);

            return jogadorComAcaoPendente;
        }

        private MensagemServidor _criarMensagemServidor(Jogador jogador, List<Acao> acoesDisponiveis)
        {
            EscolhaServidor escolhaServidor = _criarEscolha(acoesDisponiveis);

            var mensagemServidor = new MensagemServidor(
                jogador.Id,
                IdMesa,
                jogador.AcoesDisponiveis,
                jogador.CalcularTesouros(),
                _eventosAcaoAtual,
                escolhaServidor,
                string.Empty);

            return mensagemServidor;
        }

        private EscolhaServidor _criarEscolha(List<Acao> acoesDisponiveis)
        {
            if (acoesDisponiveis.Count == 0)
                return null;

            throw new NotImplementedException();
        }

        private void _aoAdicionarCartaNaMao(Guid idJogador, Carta carta)
        {
            _adicionaEvento(idJogador, LocalEvento.Mao, carta.Id, true);
        }

        private void _aoRemoverCartaNaMao(Guid idJogador, Carta carta)
        {
            _adicionaEvento(idJogador, LocalEvento.Mao, carta.Id, false);
        }

        private void _aoAdicionarCartaNoCampo(Guid idJogador, Carta carta)
        {
            _adicionaEvento(idJogador, LocalEvento.Campo, carta.Id, true);
        }

        private void _aoRemoverCartaNoCampo(Guid idJogador, Carta carta)
        {
            _adicionaEvento(idJogador, LocalEvento.Campo, carta.Id, false);
        }

        private void _adicionaEvento(
            Guid idJogador,
            LocalEvento localEvento,
            string idCarta,
            bool adicionado)
        {
            var evento = new Evento(localEvento, idCarta, adicionado);

            _eventosAcaoAtual[idJogador].Add(evento);
        }
    }
}

namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Acoes.Tipos;
    using Dominio.Cartas;
    using Dominio.Excecoes;
    using Protocolo;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Servidor;
    using Servico.Excecoes.Partida;

    internal class PartidaServico
    {
        public Guid Id { get; private set; }

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

            Id = _mesa.Id;

            _eventosAcaoAtual = new Dictionary<Guid, List<Evento>>();
            _possiveisAcoesEnviadasAosJogadores = new Dictionary<Jogador, List<Acao>>();
        }

        public List<MensagemPartidaServidor> ProcessarMensagemCliente(MensagemPartidaCliente mensagemPartidaCliente)
        {
            lock (_lockObject)
                return _processarMensagemCliente(mensagemPartidaCliente);
        }

        private List<MensagemPartidaServidor> _processarMensagemCliente(MensagemPartidaCliente mensagemPartidaCliente)
        {
            List<MensagemPartidaServidor> mensagensServidor = new List<MensagemPartidaServidor>();

            try
            {
                Jogador jogadorComAcaoPendente = _obterJogadorComAcaoPendente(mensagemPartidaCliente);
                Acao acaoPendente = _obterAcaoPendente(jogadorComAcaoPendente, mensagemPartidaCliente);

                Dictionary<Jogador, List<Acao>> acoesPosProcessamentoAcao = _mesa.ProcessarAcao(acaoPendente);

                foreach ((Jogador jogador, List<Acao> acoesDisponiveis) in acoesPosProcessamentoAcao)
                {
                    MensagemPartidaServidor mensagemPartidaServidor = _criarMensagemServidor(jogador, acoesDisponiveis);

                    mensagensServidor.Add(mensagemPartidaServidor);

                    _eventosAcaoAtual.Clear();
                    _possiveisAcoesEnviadasAosJogadores.Add(jogador, acoesDisponiveis);
                }

                _possiveisAcoesEnviadasAosJogadores[jogadorComAcaoPendente].Remove(acaoPendente);
            }
            catch (BaseServicoException servicoException)
            {
                mensagensServidor.Add(new MensagemPartidaServidor(servicoException.Id, servicoException.Message));
            }
            catch (BaseRegraExcecao regraException)
            {
                mensagensServidor.Add(new MensagemPartidaServidor(regraException.Id, regraException.Message));
            }

            return mensagensServidor;
        }

        private Acao _obterAcaoPendente(Jogador jogador, MensagemPartidaCliente mensagemPartidaCliente)
        {
            string idAcaoExecutada = mensagemPartidaCliente.IdAcaoExecutada;

            List<Acao> acoesPendentes = _possiveisAcoesEnviadasAosJogadores[jogador];

            Acao acaoPendente = acoesPendentes.FirstOrDefault(a => a.Id == idAcaoExecutada);

            if (acaoPendente == null)
                throw new AcaoNaoDisponivelExcecao(idAcaoExecutada);

            if (acaoPendente is Resultante acaoResultante)
                _preencherResultanteComEscolha(acaoResultante, mensagemPartidaCliente.EscolhaPartidaCliente);

            return acaoPendente;
        }

        private Jogador _obterJogadorComAcaoPendente(MensagemPartidaCliente mensagemPartidaCliente)
        {
            Guid idJogadorRealizador = mensagemPartidaCliente.IdJogadorRealizador;

            (Jogador jogadorComAcaoPendente, List<Acao> _) =
                _possiveisAcoesEnviadasAosJogadores.FirstOrDefault(a => a.Key.Id == idJogadorRealizador);

            if (jogadorComAcaoPendente == null)
                throw new JogadorSemAcaoPendenteExcecao(idJogadorRealizador);

            return jogadorComAcaoPendente;
        }

        private void _preencherResultanteComEscolha(
            Resultante acaoResultante,
            EscolhaPartidaCliente escolhaPartidaCliente)
        {
            string idEscolhido = escolhaPartidaCliente.Escolhido;

            switch (escolhaPartidaCliente.Tipo)
            {
                case TipoEscolha.Acao:
                    acaoResultante.PreencherAcaoEscolhida(idEscolhido);
                    break;

                case TipoEscolha.Carta:
                    acaoResultante.PreencherCartaEscolhida(idEscolhido);
                    break;

                case TipoEscolha.Jogador:
                    acaoResultante.PreencherJogadorEscolhido(idEscolhido);
                    break;

                default:
                    throw new TipoEscolhaNaoEncontrada((int)escolhaPartidaCliente.Tipo);
            }
        }

        private MensagemPartidaServidor _criarMensagemServidor(Jogador jogador, List<Acao> acoesDisponiveis)
        {
            EscolhaServidor escolhaServidor = _criarEscolha(acoesDisponiveis);

            var mensagemServidor = new MensagemPartidaServidor(
                jogador.Id,
                Id,
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
            _adicionarEvento(idJogador, LocalEvento.Mao, carta.Id, true);
        }

        private void _aoRemoverCartaNaMao(Guid idJogador, Carta carta)
        {
            _adicionarEvento(idJogador, LocalEvento.Mao, carta.Id, false);
        }

        private void _aoAdicionarCartaNoCampo(Guid idJogador, Carta carta)
        {
            _adicionarEvento(idJogador, LocalEvento.Campo, carta.Id, true);
        }

        private void _aoRemoverCartaNoCampo(Guid idJogador, Carta carta)
        {
            _adicionarEvento(idJogador, LocalEvento.Campo, carta.Id, false);
        }

        private void _adicionarEvento(
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

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
    using Escolha = Protocolo.Servidor.Escolha;

    // TODO: Talvez configurar injeção de dependência?
    public class PartidaServico
    {
        public Guid IdMesa { get; private set; }

        private Mesa _mesa { get; set; }

        private List<Acao> _possiveisAcoesEnviadasAoJogadorAtual { get; set; }

        private Dictionary<Guid, List<Evento>> _eventosAcaoAtual { get; set; }

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

            _mesa = new Mesa(jogadores);
            IdMesa = _mesa.Id;

            _possiveisAcoesEnviadasAoJogadorAtual = new List<Acao>();
        }

        public MensagemServidor ProcessaResposta(MensagemCliente mensagemClienteCliente)
        {
            string idAcao = mensagemClienteCliente.Escolha.Escolhido;
            Acao acao = _possiveisAcoesEnviadasAoJogadorAtual.FirstOrDefault(a => a.Id == idAcao);

            MensagemServidor mensagemServidor;

            try
            {
                if (acao == null)
                    throw new AcaoNaoDisponivelException(idAcao);

                List<Acao> acoesDisponiveis = _mesa.ProcessarAcao(acao);

                Jogador jogadorAtual = _mesa.JogadorAtual;

                int quantidadeAcoesDisponiveis = jogadorAtual.AcoesDisponiveis;

                Escolha escolha = null;

                if (quantidadeAcoesDisponiveis >= 1)
                    escolha = _criaEscolha(acoesDisponiveis);

                mensagemServidor = new MensagemServidor(
                    mensagemClienteCliente.IdJogadorRealizador,
                    IdMesa,
                    quantidadeAcoesDisponiveis,
                    jogadorAtual.CalcularTesouros(),
                    _eventosAcaoAtual,
                    escolha,
                    string.Empty
                );

                _eventosAcaoAtual.Clear();
                _possiveisAcoesEnviadasAoJogadorAtual = acoesDisponiveis;
            }
            catch (BaseServicoException servicoException)
            {
                mensagemServidor = new MensagemServidor(servicoException.Id);
            }
            catch (BaseRegraException regraException)
            {
                mensagemServidor = new MensagemServidor(regraException.Id);
            }

            return mensagemServidor;
        }

        private Escolha _criaEscolha(List<Acao> acoesDisponiveis)
        {
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

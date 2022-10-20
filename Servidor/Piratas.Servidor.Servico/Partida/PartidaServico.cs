namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Acoes.Resultante.Base;
    using Dominio.Cartas;
    using Dominio.Excecoes;
    using Excecoes.Partida;
    using Protocolo;
    using Protocolo.Partida;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Cliente.Escolha;
    using Protocolo.Partida.Servidor;
    using Protocolo.Partida.Servidor.Escolha;

    internal class PartidaServico
    {
        public Guid Id { get; private set; }

        private readonly Mesa _mesa;

        private readonly Dictionary<Jogador, List<Acao>> _possiveisAcoesEnviadasAosJogadores;

        private readonly Dictionary<Guid, List<Evento>> _eventosAcaoAtual;

        private readonly object _lockObject;

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

            _lockObject = new object();

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
            var mensagensServidor = new List<MensagemPartidaServidor>();

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

            switch (acaoPendente)
            {
                case BaseResultanteComDicionarioEscolhas resultanteComDicionarioEscolhas:
                    Dictionary<string, string> dicionarioEscolhas =
                        ((DicionarioEscolhasCliente)mensagemPartidaCliente.Escolha).Escolhas;

                    resultanteComDicionarioEscolhas.PreencherEscolhas(dicionarioEscolhas);
                    break;

                case BaseResultanteComEscolhaBooleana resultanteComEscolhaBooleana:
                    bool escolha = ((UmaEscolhaBooleanaCliente)mensagemPartidaCliente.Escolha).Escolha;

                    resultanteComEscolhaBooleana.PreencherEscolha(escolha);
                    break;

                case BaseResultanteComListaEscolhas resultanteComListaEscolhas:
                    List<string> escolhas = ((ListaEscolhasCliente)mensagemPartidaCliente.Escolha).Escolhas;

                    resultanteComListaEscolhas.PreencherEscolhas(escolhas);
                    break;
            }

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

        private MensagemPartidaServidor _criarMensagemServidor(Jogador jogador, List<Acao> acoesDisponiveis)
        {
            BaseEscolha escolha = _criarEscolha(acoesDisponiveis);

            var mensagemServidor = new MensagemPartidaServidor(
                jogador.Id,
                Id,
                jogador.AcoesDisponiveis,
                jogador.CalcularTesouros(),
                _eventosAcaoAtual,
                escolha,
                string.Empty);

            return mensagemServidor;
        }

        private BaseEscolha _criarEscolha(List<Acao> acoesDisponiveis)
        {
            if (acoesDisponiveis.Count == 0)
                return null;

            if (acoesDisponiveis.All(a => a is Primaria))
            {
                List<string> idsAcoes = acoesDisponiveis.Select(a => Id.ToString()).ToList();

                var umaEscolha = new ListaEscolhasServidor(TipoEscolha.Acao, idsAcoes);

                return umaEscolha;
            }

            var resultante = (BaseResultante)acoesDisponiveis[0];

            BaseEscolha escolhaResultante = _criarEscolhaDeResultante(resultante);

            return escolhaResultante;
        }

        private BaseEscolha _criarEscolhaDeResultante(BaseResultante resultante)
        {
            BaseEscolha escolhaResultante;

            switch (resultante)
            {
                case BaseResultanteComDicionarioEscolhas resultanteComDicionarioEscolhas:
                    escolhaResultante = new DicionarioEscolhasServidor(
                        _obterTipoEscolhaProtocolo(resultanteComDicionarioEscolhas.TipoEscolha),
                        _obterTipoEscolhaProtocolo(resultanteComDicionarioEscolhas.TipoEscolhaChaves),
                        _obterTipoEscolhaProtocolo(resultanteComDicionarioEscolhas.TipoEscolhaOpcoes),
                        resultanteComDicionarioEscolhas.LimiteValoresPorChave,
                        resultanteComDicionarioEscolhas.OpcoesChaves,
                        resultanteComDicionarioEscolhas.OpcoesValores);

                    break;

                case BaseResultanteComEscolhaBooleana resultanteComEscolhaBooleana:
                    escolhaResultante = new UmaEscolhaBooleanaServidor(
                        _obterTipoEscolhaProtocolo(resultanteComEscolhaBooleana.TipoEscolha));
                    break;

                case BaseResultanteComListaEscolhas resultanteComListaEscolhas:
                    escolhaResultante = new ListaEscolhasServidor(
                        _obterTipoEscolhaProtocolo(resultanteComListaEscolhas.TipoEscolha),
                        resultanteComListaEscolhas.Opcoes,
                        resultanteComListaEscolhas.LimiteEscolhas);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(resultante));
            }

            return escolhaResultante;
        }

        private TipoEscolha _obterTipoEscolhaProtocolo(Dominio.Acoes.Resultante.Enums.TipoEscolha tipoEscolha)
        {
            return tipoEscolha switch
            {
                Dominio.Acoes.Resultante.Enums.TipoEscolha.Acao => TipoEscolha.Acao,
                Dominio.Acoes.Resultante.Enums.TipoEscolha.Jogador => TipoEscolha.Jogador,
                Dominio.Acoes.Resultante.Enums.TipoEscolha.Carta => TipoEscolha.Carta,
                _ => throw new ArgumentOutOfRangeException(nameof(tipoEscolha), tipoEscolha, null)
            };
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

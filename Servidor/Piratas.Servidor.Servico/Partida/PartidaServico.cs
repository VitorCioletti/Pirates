namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Acoes.Primaria;
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

    internal sealed class PartidaServico
    {
        public Guid Id { get; }

        private readonly Mesa _mesa;

        private readonly Dictionary<Jogador, List<BaseAcao>> _possiveisAcoesEnviadasAosJogadores;

        private readonly Dictionary<string, List<Evento>> _eventosAcaoAtual;

        private readonly object _lockObject;

        public PartidaServico(List<string> idsJogadores)
        {
            var jogadores = new List<Jogador>();

            foreach (string idJogador in idsJogadores)
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

            _eventosAcaoAtual = new Dictionary<string, List<Evento>>();
            _possiveisAcoesEnviadasAosJogadores = new Dictionary<Jogador, List<BaseAcao>>();
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
                switch (mensagemPartidaCliente.TipoMensagem)
                {
                    case TipoMensagemCliente.ObterEstadoAtualMesa:
                        return _obterEstadoAtualMesa();

                    case TipoMensagemCliente.Escolher:
                        Jogador jogadorComAcaoPendente = _obterJogadorComAcaoPendente(mensagemPartidaCliente);
                        BaseAcao baseAcaoPendente = _obterAcaoPendente(jogadorComAcaoPendente, mensagemPartidaCliente);

                        Dictionary<Jogador, List<BaseAcao>> acoesPosProcessamentoAcao =
                            _mesa.ProcessarAcao(baseAcaoPendente);

                        foreach ((Jogador jogador, List<BaseAcao> acoesDisponiveis) in acoesPosProcessamentoAcao)
                        {
                            MensagemPartidaServidor mensagemPartidaServidor =
                                _criarMensagemServidor(mensagemPartidaCliente, jogador, acoesDisponiveis);

                            mensagensServidor.Add(mensagemPartidaServidor);

                            _eventosAcaoAtual.Clear();
                            _possiveisAcoesEnviadasAosJogadores.Add(jogador, acoesDisponiveis);
                        }

                        _possiveisAcoesEnviadasAosJogadores[jogadorComAcaoPendente].Remove(baseAcaoPendente);

                        break;

                    default:
                        throw new TipoMensagemNaoSuportadaExcecao();
                }
            }
            catch (BaseServicoException servicoException)
            {
                mensagensServidor.Add(
                    new MensagemPartidaServidor(
                        mensagemPartidaCliente.Id,
                        servicoException.Id,
                        servicoException.Message));
            }
            catch (BaseDominioExcecao regraException)
            {
                mensagensServidor.Add(
                    new MensagemPartidaServidor(
                        mensagemPartidaCliente.Id,
                        regraException.Id,
                        regraException.Message));
            }

            return mensagensServidor;
        }

        private BaseAcao _obterAcaoPendente(Jogador jogador, MensagemPartidaCliente mensagemPartidaCliente)
        {
            string idAcaoExecutada = mensagemPartidaCliente.IdAcaoExecutada;

            List<BaseAcao> acoesPendentes = _possiveisAcoesEnviadasAosJogadores[jogador];

            BaseAcao baseAcaoPendente = acoesPendentes.FirstOrDefault(a => a.Id == idAcaoExecutada);

            if (baseAcaoPendente == null)
                throw new AcaoNaoDisponivelExcecao(idAcaoExecutada);

            switch (baseAcaoPendente)
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

            return baseAcaoPendente;
        }

        private Jogador _obterJogadorComAcaoPendente(MensagemPartidaCliente mensagemPartidaCliente)
        {
            string idJogadorRealizador = mensagemPartidaCliente.IdJogadorRealizador;

            (Jogador jogadorComAcaoPendente, List<BaseAcao> _) =
                _possiveisAcoesEnviadasAosJogadores.FirstOrDefault(a => a.Key.Id == idJogadorRealizador);

            if (jogadorComAcaoPendente == null)
                throw new JogadorSemAcaoPendenteExcecao(idJogadorRealizador);

            return jogadorComAcaoPendente;
        }

        private MensagemPartidaServidor _criarMensagemServidor(
            BaseMensagem mensagemCliente,
            Jogador jogador,
            List<BaseAcao> acoesDisponiveis)
        {
            BaseEscolha escolha = _criarEscolha(acoesDisponiveis);

            var mensagemServidor = new MensagemPartidaServidor(
                jogador.Id,
                Id,
                mensagemCliente.Id,
                jogador.AcoesDisponiveis,
                jogador.CalcularTesouros(),
                _eventosAcaoAtual,
                escolha,
                string.Empty);

            return mensagemServidor;
        }

        private BaseEscolha _criarEscolha(List<BaseAcao> acoesDisponiveis)
        {
            if (acoesDisponiveis.Count == 0)
                return null;

            if (acoesDisponiveis.All(a => a is BasePrimaria))
            {
                List<string> idsAcoes = acoesDisponiveis.Select(a => a.Id.ToString()).ToList();

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

        private void _aoAdicionarCartaNaMao(string idJogador, Carta carta)
        {
            _adicionarEvento(
                idJogador,
                LocalEvento.Mao,
                carta.Id,
                true);
        }

        private void _aoRemoverCartaNaMao(string idJogador, Carta carta)
        {
            _adicionarEvento(
                idJogador,
                LocalEvento.Mao,
                carta.Id,
                false);
        }

        private void _aoAdicionarCartaNoCampo(string idJogador, Carta carta)
        {
            _adicionarEvento(
                idJogador,
                LocalEvento.Campo,
                carta.Id,
                true);
        }

        private void _aoRemoverCartaNoCampo(string idJogador, Carta carta)
        {
            _adicionarEvento(
                idJogador,
                LocalEvento.Campo,
                carta.Id,
                false);
        }

        private void _adicionarEvento(
            string idJogador,
            LocalEvento localEvento,
            string idCarta,
            bool adicionado)
        {
            var evento = new Evento(localEvento, idCarta, adicionado);

            _eventosAcaoAtual[idJogador].Add(evento);
        }

        private List<MensagemPartidaServidor> _obterEstadoAtualMesa()
        {
            var mensagens = new List<MensagemPartidaServidor>();

            foreach (Jogador jogador in _mesa.Jogadores)
            {
                BaseEscolha escolha = null;

                if (jogador == _mesa.JogadorAtual)
                {
                    List<BasePrimaria> acoesPrimarias = _mesa.ObterAcoesPrimarias();

                    List<string> idAcoesPrimarias = acoesPrimarias.Select(a => a.Id).ToList();

                    escolha = new ListaEscolhasCliente(TipoEscolha.Acao, idAcoesPrimarias);
                }

                Dictionary<string, List<Evento>> eventos = _obterEventosInicioPartida(jogador);

                var mensagemPartidaServidor = new MensagemPartidaServidor(
                    jogador.Id,
                    _mesa.Id,
                    Guid.Empty,
                    jogador.AcoesDisponiveis,
                    jogador.CalcularTesouros(),
                    eventos,
                    escolha);

                mensagens.Add(mensagemPartidaServidor);
            }

            return mensagens;
        }

        private Dictionary<string, List<Evento>> _obterEventosInicioPartida(Jogador receptor)
        {
            var eventos = new Dictionary<string, List<Evento>>();

            foreach (Jogador jogador in _mesa.Jogadores)
            {
                var eventosCartasAdicionadas = new List<Evento>();

                foreach (Carta carta in jogador.Mao.ObterTodas())
                {
                    bool deveMostrarIdCarta = jogador.Id == receptor.Id;

                    string idCarta = deveMostrarIdCarta ? carta.Id : string.Empty;

                    var evento = new Evento(LocalEvento.Mao, idCarta, true);

                    eventosCartasAdicionadas.Add(evento);
                }

                eventos[jogador.Id] = eventosCartasAdicionadas;
            }

            return eventos;
        }
    }
}

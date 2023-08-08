namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configuracao;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Acoes.Primaria;
    using Dominio.Acoes.Resultante.Base;
    using Dominio.Baralhos;
    using Dominio.Cartas;
    using Dominio.Excecoes;
    using Excecoes.Partida;
    using Microsoft.Extensions.Configuration;
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

        private readonly Dictionary<Jogador, List<BaseAcao>> _acoesDisponiveisEnviadasAosJogadores;

        private readonly Dictionary<string, List<Evento>> _eventosAcaoAtual;

        private readonly object _lockObject;

        public PartidaServico(List<string> idsJogadores)
        {
            _eventosAcaoAtual = new Dictionary<string, List<Evento>>();

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

                _eventosAcaoAtual[jogador.Id] = new List<Evento>();
            }

            _lockObject = new object();

            _mesa = new Mesa(jogadores);

            Id = _mesa.Id;

            _acoesDisponiveisEnviadasAosJogadores = new Dictionary<Jogador, List<BaseAcao>>();
        }

        public static void ConfigurarGeradorCartas()
        {
            var configuracaoCartas = new List<Tuple<string, int>>();

            IConfigurationSection baralho = ConfiguracaoServico.Dados.GetSection("Baralho");

            IEnumerable<IConfigurationSection> tiposCartas = baralho.GetChildren();

            foreach (IConfigurationSection tipoCarta in tiposCartas)
            {
                IEnumerable<IConfigurationSection> cartas = tipoCarta.GetChildren();

                foreach (IConfigurationSection carta in cartas)
                {
                    string nomeCarta = carta.Key;
                    string quantidadeCarta = carta.Value;

                    if (int.TryParse(quantidadeCarta, out int quantidade))
                    {
                        var configuracao = new Tuple<string, int>(nomeCarta, quantidade);

                        configuracaoCartas.Add(configuracao);
                    }
                    else
                        throw new InvalidOperationException($"Carta \"{nomeCarta}\" mal configurada.");
                }
            }

            GeradorCartas.Configurar(configuracaoCartas);
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
                                _criarMensagemServidor(jogador, acoesDisponiveis);

                            mensagensServidor.Add(mensagemPartidaServidor);

                            _eventosAcaoAtual.Clear();
                            _acoesDisponiveisEnviadasAosJogadores.Add(jogador, acoesDisponiveis);
                        }

                        _acoesDisponiveisEnviadasAosJogadores[jogadorComAcaoPendente].Remove(baseAcaoPendente);

                        break;

                    default:
                        throw new TipoMensagemNaoSuportadaExcecao();
                }
            }
            catch (BaseServicoExcecao servicoException)
            {
                mensagensServidor.Add(new MensagemPartidaServidor(servicoException.Id, servicoException.Message));
            }
            catch (BaseDominioExcecao regraException)
            {
                mensagensServidor.Add(new MensagemPartidaServidor(regraException.Id, regraException.Message));
            }

            return mensagensServidor;
        }

        private BaseAcao _obterAcaoPendente(Jogador jogador, MensagemPartidaCliente mensagemPartidaCliente)
        {
            string idAcaoExecutada = mensagemPartidaCliente.IdAcaoExecutada;

            List<BaseAcao> acoesPendentes = _acoesDisponiveisEnviadasAosJogadores[jogador];

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
                _acoesDisponiveisEnviadasAosJogadores.FirstOrDefault(a => a.Key.Id == idJogadorRealizador);

            if (jogadorComAcaoPendente == null)
                throw new JogadorSemAcaoPendenteExcecao(idJogadorRealizador);

            return jogadorComAcaoPendente;
        }

        private MensagemPartidaServidor _criarMensagemServidor(Jogador jogador, List<BaseAcao> acoesDisponiveis)
        {
            BaseEscolha escolha = _criarEscolha(acoesDisponiveis);

            var mensagemServidor = new MensagemPartidaServidor(
                jogador.Id,
                Id,
                jogador.AcoesDisponiveis,
                jogador.CalcularTesouros(),
                _eventosAcaoAtual,
                escolha,
                _mesa.JogadorAtual.Id,
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
                List<BaseAcao> acoesDisponiveis = _mesa.AcoesDisponiveisJogadores[jogador];

                List<string> idAcoesDisponiveis = acoesDisponiveis.Select(a => a.Id).ToList();

                var escolha = new ListaEscolhasCliente(TipoEscolha.Acao, idAcoesDisponiveis);

                Dictionary<string, List<Evento>> eventos = _obterEventosEstadoAtualMesa(jogador);

                var mensagemPartidaServidor = new MensagemPartidaServidor(
                    jogador.Id,
                    _mesa.Id,
                    jogador.AcoesDisponiveis,
                    jogador.CalcularTesouros(),
                    eventos,
                    escolha,
                    _mesa.JogadorAtual.Id);

                mensagens.Add(mensagemPartidaServidor);
            }

            return mensagens;
        }

        private Dictionary<string, List<Evento>> _obterEventosEstadoAtualMesa(Jogador receptor)
        {
            var eventos = new Dictionary<string, List<Evento>>();

            foreach (Jogador jogador in _mesa.Jogadores)
            {
                var eventosCartasAdicionadas = new List<Evento>();

                IEnumerable<Evento> eventosMao = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Mao,
                    jogador.Mao.ObterTodas());

                IEnumerable<Evento> eventosCampoCanhao = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Campo,
                    jogador.Campo.Canhoes);

                IEnumerable<Evento> eventosCampoProtegidas = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Campo,
                    jogador.Campo.Protegidas);

                IEnumerable<Evento> eventosCampoDuelosSurpresa = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Campo,
                    jogador.Campo.DuelosSurpresa);

                IEnumerable<Evento> eventosCampoTripulacao = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Campo,
                    jogador.Campo.Tripulacao);

                IEnumerable<Evento> eventosCampoEmbarcacao = _criarEventos(
                    jogador,
                    receptor,
                    LocalEvento.Campo,
                    new List<Carta> {jogador.Campo.Embarcacao});

                eventosCartasAdicionadas.AddRange(eventosMao);

                eventosCartasAdicionadas.AddRange(eventosCampoCanhao);
                eventosCartasAdicionadas.AddRange(eventosCampoProtegidas);
                eventosCartasAdicionadas.AddRange(eventosCampoDuelosSurpresa);
                eventosCartasAdicionadas.AddRange(eventosCampoTripulacao);
                eventosCartasAdicionadas.AddRange(eventosCampoEmbarcacao);

                eventos[jogador.Id] = eventosCartasAdicionadas;
            }

            return eventos;
        }

        private IEnumerable<Evento> _criarEventos(
            Jogador jogador,
            Jogador receptor,
            LocalEvento localEvento,
            IEnumerable<Carta> cartas)
        {
            var eventosCartasAdicionadas = new List<Evento>();

            foreach (Carta carta in cartas)
            {
                bool deveMostrarIdCarta = jogador.Id == receptor.Id;

                string idCarta = deveMostrarIdCarta ? carta.Id : string.Empty;

                var evento = new Evento(localEvento, idCarta, true);

                eventosCartasAdicionadas.Add(evento);
            }

            return eventosCartasAdicionadas;
        }
    }
}

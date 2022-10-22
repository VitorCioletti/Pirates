namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Passiva;
    using Acoes.Primaria;
    using Acoes.Resultante.Base;
    using Baralhos.Tipos;
    using Cartas;
    using Cartas.Tipos;
    using Excecoes.Mesa;

    public class Mesa
    {
        public Guid Id { get; private set; }

        public List<Jogador> Jogadores { get; private set; }

        private DateTime _dataHoraInicio { get; set; }

        private DateTime _dataHoraFim { get; set; }

        private Jogador _jogadorAtual { get; set; }

        private bool _emDuelo { get; set; }

        private Tuple<Jogador, Jogador> _duelistas { get; set; }

        private Queue<Jogador> _ordemDeJogadores { get; set; }

        public BaralhoCentral BaralhoCentral { get; private set; }

        public PilhaDescarte PilhaDescarte { get; private set; }

        public Stack<Acao> HistoricoAcao { get; private set; }

        private Imediata _imediataAposResultantes;

        private readonly List<Acao> _acoesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        private int _turnoAtual;

        public Mesa(List<Jogador> jogadores)
        {
            _acoesPendentes = new List<Acao>();
            _imediataAposResultantes = null;

            Id = Guid.NewGuid();
            _dataHoraInicio = DateTime.UtcNow;

            HistoricoAcao = new Stack<Acao>();

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            _ordemDeJogadores = _gerarOrdemDeJogadores();

            _distribuirCartas();
        }

        public Dictionary<Jogador, List<Acao>> ProcessarAcao(Acao acaoAProcessar)
        {
            acaoAProcessar.Turno = _turnoAtual;
            Jogador realizador = acaoAProcessar.Realizador;

            var acoesPorJogador = new Dictionary<Jogador, List<Acao>>();

            _verificarPrimariaJogadorAtual(acaoAProcessar);
            _verificarResultantePendente(acaoAProcessar);

            List<Acao> acoesResultadoAcaoProcessada = acaoAProcessar.AplicarRegra(this);

            HistoricoAcao.Push(acaoAProcessar);

            _processarAcoesImediatas(acoesResultadoAcaoProcessada, acoesPorJogador);

            if (acaoAProcessar is Primaria)
                realizador.SubtrairAcoesDisponiveis();

            if (_acoesPendentes.Count == 0 && _imediataAposResultantes != null)
            {
                _processarAcaoImediata(_imediataAposResultantes, acoesPorJogador);
                _imediataAposResultantes = null;
            }

            if (acoesResultadoAcaoProcessada?.Count == 0 && _jogadorAtual.AcoesDisponiveis == 0)
                acoesPorJogador = _moverParaProximoTurno();

            foreach (List<Acao> acoesPendentes in acoesPorJogador.Values)
                _acoesPendentes.AddRange(acoesPendentes);

            return acoesPorJogador;
        }

        private void _processarAcaoImediata(
            Imediata acaoImediata,
            IReadOnlyDictionary<Jogador, List<Acao>> acoesPorJogador)
        {
            _processarAcoesImediatas(new List<Imediata> {acaoImediata}, acoesPorJogador);
        }

        private void _processarAcoesImediatas(
            IEnumerable<Acao> acoesResultadoAcaoProcessada,
            IReadOnlyDictionary<Jogador, List<Acao>> acoesPorJogador)
        {
            IEnumerable<Imediata> acoesImediatas = acoesResultadoAcaoProcessada.OfType<Imediata>();

            foreach (Imediata imediataAProcessarPosAcao in acoesImediatas)
            {
                Dictionary<Jogador, List<Acao>> acoesPosImediata = ProcessarAcao(imediataAProcessarPosAcao);

                foreach ((Jogador jogador, List<Acao> acoes) in acoesPosImediata)
                    acoesPorJogador[jogador].AddRange(acoes);
            }
        }

        private Dictionary<Jogador, List<Acao>> _moverParaProximoTurno()
        {
            if (_jogadorAtual?.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisExcecao(_jogadorAtual);

            _turnoAtual++;

            Jogador proximoJogador = _obterProximoJogador();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
                Finalizar(proximoJogador);

            Embarcacao embarcacao = proximoJogador.Campo.Embarcacao;
            Dictionary<Jogador, List<Acao>> acoesPosEfeitoEmbarcacao = null;

            if (embarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, embarcacao);
                acoesPosEfeitoEmbarcacao = ProcessarAcao(aplicarEfeitoEmbarcacao);
            }

            proximoJogador.ResetarAcoesDisponiveis(_acoesPorTurno);

            return acoesPosEfeitoEmbarcacao;
        }

        public void EntrarModoDuelo(Jogador realizador, Jogador alvo)
        {
            if (_emDuelo)
                throw new EmDueloExcecao();

            _emDuelo = true;
            _duelistas = new Tuple<Jogador, Jogador>(realizador, alvo);
        }

        public void SairModoDuelo()
        {
            if (!_emDuelo)
                throw new SemDueloExcecao();

            // TODO: Verificar se todas as ações resposta de duelo foram realizadas?

            _emDuelo = false;
            _duelistas = null;
        }

        public void Finalizar(Jogador _) => _dataHoraFim = DateTime.UtcNow;

        public void RegistrarImediataAposResultantes(Imediata imediata)
        {
            if (_imediataAposResultantes != null)
                throw new ImediataRegistradaExcecao();

            _imediataAposResultantes = imediata;
        }

        private Queue<Jogador> _gerarOrdemDeJogadores() => new(Jogadores);

        private Jogador _obterProximoJogador()
        {
            Jogador proximoJogador = _ordemDeJogadores.Dequeue();
            _ordemDeJogadores.Enqueue(proximoJogador);

            _jogadorAtual = proximoJogador;

            return proximoJogador;
        }

        private void _distribuirCartas()
        {
            foreach (Jogador jogador in Jogadores)
            {
                List<Carta> cartas = BaralhoCentral.ObterTopo(_cartasIniciaisPorJogador);

                jogador.Mao.Adicionar(cartas);
            }
        }

        private void _verificarPrimariaJogadorAtual(Acao acao)
        {
            Jogador realizador = acao.Realizador;

            if (acao is Primaria)
            {
                if (realizador != _jogadorAtual)
                    throw new TurnoDeOutroJogadorExcecao(realizador);
            }
        }

        private void _verificarResultantePendente(Acao acao)
        {
            if (acao is BaseResultante resultante)
            {
                if (!_acoesPendentes.Contains(resultante))
                    throw new ResultanteNaoEsperadaExcecao(resultante);
            }
        }
    }
}

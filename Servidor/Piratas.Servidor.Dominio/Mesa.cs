namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Passiva;
    using Acoes.Primaria;
    using Acoes.Resultante.Base;
    using Baralhos;
    using Cartas;
    using Cartas.Embarcacao;
    using Excecoes.Mesa;

    public class Mesa
    {
        public Guid Id { get; private set; }

        public List<Jogador> Jogadores { get; private set; }

        public Jogador JogadorAtual { get; private set; }

        private DateTime _dataHoraInicio { get; set; }

        private DateTime _dataHoraFim { get; set; }

        private bool _emDuelo { get; set; }

        private Tuple<Jogador, Jogador> _duelistas { get; set; }

        private Queue<Jogador> _ordemDeJogadores { get; set; }

        public BaralhoCentral BaralhoCentral { get; private set; }

        public PilhaDescarte PilhaDescarte { get; private set; }

        public Stack<BaseAcao> HistoricoAcao { get; private set; }

        private BaseImediata _baseImediataAposResultantes;

        private readonly List<BaseAcao> _acoesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        private int _turnoAtual;

        public Mesa(List<Jogador> jogadores)
        {
            _acoesPendentes = new List<BaseAcao>();
            _baseImediataAposResultantes = null;

            Id = Guid.NewGuid();
            _dataHoraInicio = DateTime.UtcNow;

            HistoricoAcao = new Stack<BaseAcao>();

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            _ordemDeJogadores = _gerarOrdemDeJogadores();

            _distribuirCartas();
        }

        public Dictionary<Jogador, List<BaseAcao>> ProcessarAcao(BaseAcao baseAcaoAProcessar)
        {
            baseAcaoAProcessar.Turno = _turnoAtual;
            Jogador realizador = baseAcaoAProcessar.Realizador;

            var acoesPorJogador = new Dictionary<Jogador, List<BaseAcao>>();

            _verificarPrimariaJogadorAtual(baseAcaoAProcessar);
            _verificarResultantePendente(baseAcaoAProcessar);

            List<BaseAcao> acoesResultadoAcaoProcessada = baseAcaoAProcessar.AplicarRegra(this);

            HistoricoAcao.Push(baseAcaoAProcessar);

            _processarAcoesImediatas(acoesResultadoAcaoProcessada, acoesPorJogador);

            if (baseAcaoAProcessar is BasePrimaria)
                realizador.SubtrairAcoesDisponiveis();

            if (_acoesPendentes.Count == 0 && _baseImediataAposResultantes != null)
            {
                _processarAcaoImediata(_baseImediataAposResultantes, acoesPorJogador);
                _baseImediataAposResultantes = null;
            }

            if (acoesResultadoAcaoProcessada?.Count == 0 && JogadorAtual.AcoesDisponiveis == 0)
                acoesPorJogador = _moverParaProximoTurno();

            foreach (List<BaseAcao> acoesPendentes in acoesPorJogador.Values)
                _acoesPendentes.AddRange(acoesPendentes);

            return acoesPorJogador;
        }

        public List<BasePrimaria> ObterAcoesPrimarias()
        {
            var acoes = new List<BasePrimaria>();

            acoes.Add(new Duelar(null, null, null));
            acoes.Add(new DescerCarta(null, null));
            acoes.Add(new ComprarCarta(null));

            return acoes;
        }

        private void _processarAcaoImediata(
            BaseImediata acaoBaseImediata,
            IReadOnlyDictionary<Jogador, List<BaseAcao>> acoesPorJogador)
        {
            _processarAcoesImediatas(new List<BaseImediata> {acaoBaseImediata}, acoesPorJogador);
        }

        private void _processarAcoesImediatas(
            IEnumerable<BaseAcao> acoesResultadoAcaoProcessada,
            IReadOnlyDictionary<Jogador, List<BaseAcao>> acoesPorJogador)
        {
            IEnumerable<BaseImediata> acoesImediatas = acoesResultadoAcaoProcessada.OfType<BaseImediata>();

            foreach (BaseImediata imediataAProcessarPosAcao in acoesImediatas)
            {
                Dictionary<Jogador, List<BaseAcao>> acoesPosImediata = ProcessarAcao(imediataAProcessarPosAcao);

                foreach ((Jogador jogador, List<BaseAcao> acoes) in acoesPosImediata)
                    acoesPorJogador[jogador].AddRange(acoes);
            }
        }

        private Dictionary<Jogador, List<BaseAcao>> _moverParaProximoTurno()
        {
            if (JogadorAtual?.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisExcecao(JogadorAtual);

            _turnoAtual++;

            Jogador proximoJogador = _obterProximoJogador();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
                Finalizar(proximoJogador);

            BaseEmbarcacao baseEmbarcacao = proximoJogador.Campo.BaseEmbarcacao;
            Dictionary<Jogador, List<BaseAcao>> acoesPosEfeitoEmbarcacao = null;

            if (baseEmbarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, baseEmbarcacao);
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

            _emDuelo = false;
            _duelistas = null;
        }

        public void Finalizar(Jogador _) => _dataHoraFim = DateTime.UtcNow;

        public void RegistrarImediataAposResultantes(BaseImediata baseImediata)
        {
            if (_baseImediataAposResultantes != null)
                throw new ImediataRegistradaExcecao();

            _baseImediataAposResultantes = baseImediata;
        }

        private Queue<Jogador> _gerarOrdemDeJogadores() => new(Jogadores);

        private Jogador _obterProximoJogador()
        {
            Jogador proximoJogador = _ordemDeJogadores.Dequeue();
            _ordemDeJogadores.Enqueue(proximoJogador);

            JogadorAtual = proximoJogador;

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

        private void _verificarPrimariaJogadorAtual(BaseAcao baseAcao)
        {
            Jogador realizador = baseAcao.Realizador;

            if (baseAcao is BasePrimaria)
            {
                if (realizador != JogadorAtual)
                    throw new TurnoDeOutroJogadorExcecao(realizador);
            }
        }

        private void _verificarResultantePendente(BaseAcao baseAcao)
        {
            if (baseAcao is BaseResultante resultante)
            {
                if (!_acoesPendentes.Contains(resultante))
                    throw new ResultanteNaoEsperadaExcecao(resultante);
            }
        }
    }
}

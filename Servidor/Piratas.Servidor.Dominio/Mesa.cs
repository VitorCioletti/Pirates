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

        public List<Jogador> Jogadores { get; }

        public Jogador JogadorAtual { get; private set; }

        public BaralhoCentral BaralhoCentral { get; }

        public PilhaDescarte PilhaDescarte { get; private set; }

        public Stack<BaseAcao> HistoricoAcao { get; }

        public Dictionary<Jogador, List<BaseAcao>> AcoesDisponiveisJogadores { get; private set; }

        private bool _emDuelo { get; set; }

        private Tuple<Jogador, Jogador> _duelistas { get; set; }

        private Queue<Jogador> _ordemDeJogadores { get; }

        private BaseImediata _baseImediataAposResultantes;

        private readonly List<BaseAcao> _acoesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        private int _turnoAtual;

        public Mesa(List<Jogador> jogadores)
        {
            _acoesPendentes = new List<BaseAcao>();
            AcoesDisponiveisJogadores = new Dictionary<Jogador, List<BaseAcao>>();
            _baseImediataAposResultantes = null;

            Id = Guid.NewGuid();

            HistoricoAcao = new Stack<BaseAcao>();
            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            _ordemDeJogadores = _gerarOrdemDeJogadores();
            JogadorAtual = _ordemDeJogadores.Peek();

            AcoesDisponiveisJogadores[JogadorAtual] = _obterAcoesPrimarias();

            BaralhoCentral.GerarCartas();

            _distribuirCartas();
        }

        public Dictionary<Jogador, List<BaseAcao>> ProcessarAcao(BaseAcao acaoAProcessar)
        {
            acaoAProcessar.Turno = _turnoAtual;
            Jogador realizador = acaoAProcessar.Realizador;

            var acoesPorJogador = new Dictionary<Jogador, List<BaseAcao>>();

            _verificarPrimariaJogadorAtual(acaoAProcessar);
            _verificarResultantePendente(acaoAProcessar);

            List<BaseAcao> acoesResultadoAcaoProcessada = acaoAProcessar.AplicarRegra(this);

            HistoricoAcao.Push(acaoAProcessar);

            _processarAcoesImediatas(acoesResultadoAcaoProcessada, acoesPorJogador);

            if (acaoAProcessar is BasePrimaria)
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

        private List<BaseAcao> _obterAcoesPrimarias()
        {
            var acoes = new List<BaseAcao>
            {
                new Duelar(null, null, null), new DescerCarta(null, null), new ComprarCarta(null)
            };

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

            BaseEmbarcacao baseEmbarcacao = proximoJogador.Campo.Embarcacao;
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

        public void Finalizar(Jogador _)
        {

        }

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

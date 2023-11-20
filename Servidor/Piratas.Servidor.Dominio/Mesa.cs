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

        public int Turno { get; private set; }

        public Dictionary<Jogador, List<BaseAcao>> AcoesDisponiveisJogadores { get; private set; }

        public Jogador Vencedor { get; private set; }

        public bool EmDuelo { get; private set; }

        private Queue<Jogador> _ordemDeJogadores { get; }

        private BaseImediata _imediataAposResultantes;

        private readonly List<BaseAcao> _acoesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        public Mesa(List<Jogador> jogadores)
        {
            _acoesPendentes = new List<BaseAcao>();
            AcoesDisponiveisJogadores = new Dictionary<Jogador, List<BaseAcao>>();
            _imediataAposResultantes = null;

            Id = Guid.NewGuid();

            HistoricoAcao = new Stack<BaseAcao>();
            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            _ordemDeJogadores = _gerarOrdemDeJogadores();
            JogadorAtual = _obterProximoJogador();

            AcoesDisponiveisJogadores[JogadorAtual] = _obterAcoesPrimarias();

            foreach (Jogador jogador in Jogadores)
                jogador.ResetarAcoesDisponiveis(_acoesPorTurno);

            BaralhoCentral.GerarCartas();

            _distribuirCartas();
        }

        public Dictionary<Jogador, List<BaseAcao>> ProcessarAcao(BaseAcao acaoAProcessar)
        {
            acaoAProcessar.Turno = Turno;
            Jogador realizador = acaoAProcessar.Realizador;

            var acoesPorJogador = new Dictionary<Jogador, List<BaseAcao>>();

            _verificarPrimariaJogadorAtual(acaoAProcessar);
            _verificarResultantePendente(acaoAProcessar);

            List<BaseAcao> acoesResultadoAcaoProcessada = acaoAProcessar.AplicarRegra(this);

            HistoricoAcao.Push(acaoAProcessar);

            if (acoesResultadoAcaoProcessada != null)
            {
                List<BaseAcao> acoesImediatas = acoesResultadoAcaoProcessada.Where(a => a is BaseImediata).ToList();

                if (acoesImediatas.Count > 0)
                    _processarAcoesImediatas(acoesImediatas, acoesPorJogador);
            }

            if (acaoAProcessar is BasePrimaria)
                realizador.SubtrairAcoesDisponiveis();

            if (_acoesPendentes.Count == 0 && _imediataAposResultantes != null)
            {
                _processarAcaoImediata(_imediataAposResultantes, acoesPorJogador);
            }

            bool naoPossuiResultadoAcao =
                acoesResultadoAcaoProcessada is null || acoesResultadoAcaoProcessada.Count == 0;

            bool naoPossuiAcoesDisponiveis = JogadorAtual.AcoesDisponiveis == 0;

            if (naoPossuiResultadoAcao && naoPossuiAcoesDisponiveis)
            {
                acoesPorJogador = _moverParaProximoTurno();
            }
            else if (acoesResultadoAcaoProcessada is not null)
            {
                Jogador jogador = acoesResultadoAcaoProcessada[0].Realizador;

                acoesPorJogador.Add(jogador, acoesResultadoAcaoProcessada);
            }

            // ReSharper disable once InvertIf
            if (acoesPorJogador.Count > 0)
            {
                foreach (List<BaseAcao> acoesPendentes in acoesPorJogador.Values)
                    _acoesPendentes.AddRange(acoesPendentes);
            }

            return acoesPorJogador;
        }

        private List<BaseAcao> _obterAcoesPrimarias()
        {
            var acoes = new List<BaseAcao>
            {
                new Duelar(JogadorAtual, null, null),
                new DescerCarta(JogadorAtual, null),
                new ComprarCarta(JogadorAtual)
            };

            return acoes;
        }

        private void _processarAcaoImediata(
            BaseImediata acaoBaseImediata,
            IReadOnlyDictionary<Jogador, List<BaseAcao>> acoesPorJogador)
        {
            _imediataAposResultantes = null;
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

            Turno++;

            Jogador proximoJogador = _obterProximoJogador();

            var acoesPosEfeitoEmbarcacao = new Dictionary<Jogador, List<BaseAcao>>();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
            {
                Finalizar(proximoJogador);

                return acoesPosEfeitoEmbarcacao;
            }

            BaseEmbarcacao embarcacao = proximoJogador.Campo.Embarcacao;

            if (embarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, embarcacao);
                acoesPosEfeitoEmbarcacao = ProcessarAcao(aplicarEfeitoEmbarcacao);
            }

            proximoJogador.ResetarAcoesDisponiveis(_acoesPorTurno);

            return acoesPosEfeitoEmbarcacao;
        }

        public void EntrarModoDuelo()
        {
            if (EmDuelo)
                throw new EmDueloExcecao();

            EmDuelo = true;
        }

        public void SairModoDuelo()
        {
            if (!EmDuelo)
                throw new SemDueloExcecao();

            EmDuelo = false;
        }

        public void Finalizar(Jogador vencedor)
        {
            Vencedor = vencedor;
        }

        public void RegistrarImediataAposResultantes(BaseImediata imediata)
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

            JogadorAtual = proximoJogador;
            AcoesDisponiveisJogadores[JogadorAtual] = _obterAcoesPrimarias();

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

        private void _verificarPrimariaJogadorAtual(BaseAcao acao)
        {
            Jogador realizador = acao.Realizador;

            if (acao is not BasePrimaria)
                return;

            if (realizador != JogadorAtual)
                throw new TurnoDeOutroJogadorExcecao(realizador);
        }

        private void _verificarResultantePendente(BaseAcao acao)
        {
            if (acao is not BaseResultante resultante)
                return;

            if (!_acoesPendentes.Contains(resultante))
                throw new ResultanteNaoEsperadaExcecao(resultante);
        }
    }
}

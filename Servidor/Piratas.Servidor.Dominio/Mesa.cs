namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Passiva;
    using Acoes.Resultante.Base;
    using Baralhos.Tipos;
    using Cartas;
    using Cartas.Tipos;
    using Excecoes.Mesa;

    public class Mesa
    {
        public Guid Id { get; private set; }

        public List<Jogador> Jogadores { get; private set; }

        public DateTime DataHoraInicio { get; private set; }

        public DateTime DataHoraFim { get; private set; }

        public Jogador JogadorAtual { get; private set; }

        public bool EmDuelo { get; private set; }

        public Tuple<Jogador, Jogador> Duelistas { get; private set; }

        public Queue<Jogador> OrdemDeJogadores { get; private set; }

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
            DataHoraInicio = DateTime.UtcNow;

            HistoricoAcao = new Stack<Acao>();

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _gerarOrdemDeJogadores();

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

            if (acoesResultadoAcaoProcessada?.Count == 0 && JogadorAtual.AcoesDisponiveis == 0)
                acoesPorJogador = _moverParaProximoTurno();

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
            if (JogadorAtual.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisExcecao(JogadorAtual);

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
            if (EmDuelo)
                throw new EmDueloExcecao();

            EmDuelo = true;
            Duelistas = new Tuple<Jogador, Jogador>(realizador, alvo);
        }

        public void SairModoDuelo()
        {
            if (!EmDuelo)
                throw new SemDueloExcecao();

            // TODO: Verificar se todas as ações resposta de duelo foram realizadas?

            EmDuelo = false;
            Duelistas = null;
        }

        public void Finalizar(Jogador _) => DataHoraFim = DateTime.UtcNow;

        public void RegistrarImediataAposResultantes(Imediata imediata)
        {
            if (_imediataAposResultantes != null)
                throw new ImediataRegistradaExcecao();

            _imediataAposResultantes = imediata;
        }

        private Queue<Jogador> _gerarOrdemDeJogadores() => new(Jogadores);

        private Jogador _obterProximoJogador()
        {
            Jogador proximoJogador = OrdemDeJogadores.Dequeue();
            OrdemDeJogadores.Enqueue(proximoJogador);

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

        private void _verificarPrimariaJogadorAtual(Acao acao)
        {
            Jogador realizador = acao.Realizador;

            if (acao is Primaria)
            {
                if (realizador != JogadorAtual)
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

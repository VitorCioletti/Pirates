namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Passiva;
    using Acoes.Tipos;
    using Baralhos.Tipos;
    using Excecoes.Mesa;

    public class Mesa
    {
        public string Id { get; private set; }

        public List<Jogador> Jogadores { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public Jogador JogadorAtual { get; set; }

        public bool EmDuelo { get; private set; }

        public Tuple<Jogador, Jogador> Duelistas { get; private set; }

        public Queue<Jogador> OrdemDeJogadores { get; private set; }

        public BaralhoCentral BaralhoCentral { get; private set; }

        public PilhaDescarte PilhaDescarte { get; set; }

        public Stack<Acao> HistoricoAcao { get; private set; }

        private Imediata _imediataAposResultantes;

        private readonly List<Resultante> _resultantesPendentes;

        private readonly int _cartasIniciaisPorJogador;

        private readonly int _tesourosParaVitoria;

        private int _turnoAtual;

        public Mesa(List<Jogador> jogadores)
        {
            _cartasIniciaisPorJogador = 5;
            _tesourosParaVitoria = 5;
            _turnoAtual = 1;

            _resultantesPendentes = new List<Resultante>();
            _imediataAposResultantes = null;

            Id = Guid.NewGuid().ToString();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _gerarOrdemDeJogadores(jogadores);

            _distribuirCartas();
        }

        public IList<Resultante> ProcessarAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            _verificarPrimariaJogadorAtual(acao);
            _verificarResultantePendente(acao);

            var acoesResultantes = acao.AplicarRegra(this).ToList();

            HistoricoAcao.Push(acao);

            foreach (var acaoResultante in acoesResultantes)
            {
                if (acaoResultante is Imediata)
                    acoesResultantes.AddRange(ProcessarAcao(acaoResultante));
            }

            if (acao is Primaria)
                realizador.AcoesDisponiveis--;

            else if (acao is Resultante resultante)
            {
                _resultantesPendentes.Remove(resultante);

                if (_resultantesPendentes.Count == 0 && _imediataAposResultantes != null)
                    acoesResultantes.AddRange(ProcessarAcao(_imediataAposResultantes));
            }

            acao.Turno = _turnoAtual;

            _resultantesPendentes.AddRange(acoesResultantes);

            return acoesResultantes;
        }

        public Tuple<Jogador, IEnumerable<Resultante>> MoverParaProximoTurno()
        {
            if (JogadorAtual.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisException(JogadorAtual);

            _turnoAtual++;

            var proximoJogador = _obterProximoJogador();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
                Finalizar(proximoJogador);

            var embarcacao = proximoJogador.Campo.Embarcacao;
            IEnumerable<Resultante> resultantesEmbarcacao = null;

            if (embarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, embarcacao);
                resultantesEmbarcacao = ProcessarAcao(aplicarEfeitoEmbarcacao);
            }

            return new Tuple<Jogador, IEnumerable<Resultante>>(proximoJogador, resultantesEmbarcacao);
        }

        public void EntrarModoDuelo(Jogador realizador, Jogador alvo)
        {
            if (EmDuelo)
                throw new EmDueloException();

            EmDuelo = true;
            Duelistas = new Tuple<Jogador, Jogador>(realizador, alvo);
        }

        public void SairModoDuelo()
        {
            if (!EmDuelo)
                throw new SemDueloException();

            // TODO: Verificar se todas as ações resposta de duelo foram realizadas?

            EmDuelo = false;
            Duelistas = null;
        }

        public void Finalizar(Jogador _) => DataHoraFim = DateTime.UtcNow;

        public void RegistrarImediataAposResultantes(Imediata imediata)
        {
            if (_imediataAposResultantes != null)
                throw new ImediataRegistradaException();

            _imediataAposResultantes = imediata;
        }

        public void RemoverImediataAposResultantes() => _imediataAposResultantes = null;

        private Queue<Jogador> _gerarOrdemDeJogadores(List<Jogador> jogadores) => new Queue<Jogador>(jogadores);

        private Jogador _obterProximoJogador()
        {
            var proximoJogador = OrdemDeJogadores.Dequeue();
            OrdemDeJogadores.Enqueue(proximoJogador);

            JogadorAtual = proximoJogador;

            return proximoJogador;
        }

        private void _distribuirCartas()
        {
            foreach (var jogador in Jogadores)
            {
                var cartas = BaralhoCentral.ObterTopo(_cartasIniciaisPorJogador);

                jogador.Mao.Adicionar(cartas);
            }
        }

        private void _verificarPrimariaJogadorAtual(Acao acao)
        {
            var realizador = acao.Realizador;

            if (acao is Primaria)
            {
                if (realizador != JogadorAtual)
                    throw new TurnoDeOutroJogadorException(realizador);
            }
        }

        private void _verificarResultantePendente(Acao acao)
        {
            if (acao is Resultante resultante)
            {
                if (!_resultantesPendentes.Contains(resultante))
                    throw new ResultanteNaoEsperadaException(resultante);
            }
        }
    }
}

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
        public Guid Id { get; private set; }

        public List<Jogador> Jogadores { get; private set; }

        public DateTime DataHoraInicio { get; private set; }

        public DateTime DataHoraFim { get; private set; }

        public Jogador JogadorAtual { get; private set; }

        public bool EmDuelo { get; private set; }

        public Tuple<Jogador, Jogador> Duelistas { get; private set; }

        public Queue<Jogador> OrdemDeJogadores { get; }

        public BaralhoCentral BaralhoCentral { get; }

        public PilhaDescarte PilhaDescarte { get; }

        public Stack<Acao> HistoricoAcao { get; private set; }

        private Imediata _imediataAposResultantes;

        private readonly List<Resultante> _resultantesPendentes;

        private const int _cartasIniciaisPorJogador = 5;

        private const int _tesourosParaVitoria = 5;

        private const int _acoesPorTurno = 3;

        private int _turnoAtual = 0;

        public Mesa(List<Jogador> jogadores)
        {
            _resultantesPendentes = new List<Resultante>();
            _imediataAposResultantes = null;

            Id = Guid.NewGuid();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _gerarOrdemDeJogadores(jogadores);

            _distribuirCartas();
        }

        // TODO: Refatorar toda a lógica de múltiplas ações resultantes gerada pela introdução do Kraken.
        public List<Acao> ProcessarAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            _verificarPrimariaJogadorAtual(acao);
            _verificarResultantePendente(acao);

            var acoesResultantes = acao.AplicarRegra(this).ToList();

            HistoricoAcao.Push(acao);

            foreach (var acaoResultante in acoesResultantes)
            {
                //if (acaoResultante is Imediata)
                //   acoesResultantes.AddRange(ProcessarAcao(acaoResultante));
            }

            if (acao is Primaria)
                realizador.SubtrairAcoesDisponiveis();

            else if (acao is Resultante resultante)
            {
                _resultantesPendentes.Remove(resultante);

                //if (_resultantesPendentes.Count == 0 && _imediataAposResultantes != null)
                //    acoesResultantes.AddRange(ProcessarAcao(_imediataAposResultantes));
            }

            acao.Turno = _turnoAtual;

            _resultantesPendentes.AddRange(acoesResultantes);

            return null;
        }

        public Tuple<Jogador, Resultante> MoverParaProximoTurno()
        {
            if (JogadorAtual.AcoesDisponiveis > 0)
                throw new PossuiAcoesDisponiveisException(JogadorAtual);

            _turnoAtual++;

            var proximoJogador = _obterProximoJogador();

            if (proximoJogador.CalcularTesouros() >= _tesourosParaVitoria)
                Finalizar(proximoJogador);

            var embarcacao = proximoJogador.Campo.Embarcacao;
            Resultante resultanteEmbarcacao = null;

            if (embarcacao != null)
            {
                var aplicarEfeitoEmbarcacao = new AplicarEfeitoEmbarcacao(proximoJogador, embarcacao);
                resultanteEmbarcacao = (Resultante)ProcessarAcao(aplicarEfeitoEmbarcacao).First();
            }

            proximoJogador.ResetarAcoesDisponiveis(_acoesPorTurno);

            return new Tuple<Jogador, Resultante>(proximoJogador, resultanteEmbarcacao);
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

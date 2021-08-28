namespace Piratas.Servidor.Regras
{
    using Acoes.Passiva;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using System.Collections.Generic;
    using System.Linq;
    using System;

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

        private List<Resultante> _resultantesPendentes;
 
        private int _cartasIniciaisPorJogador;

        private int _tesourosParaVitoria;

        private int _turnoAtual;

        public Mesa(List<Jogador> jogadores)
        {
            _cartasIniciaisPorJogador = 5;
            _tesourosParaVitoria = 5;
            _turnoAtual = 1;

            _resultantesPendentes = new List<Resultante>();

            Id = Guid.NewGuid().ToString();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _gerarOrdemDeJogadores(jogadores);

            _distribuirCartas();
        }

        public IEnumerable<Resultante> ProcessarAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            _verificarPrimariaJogadorAtual(acao);
            _verificarResultantePendente(acao);

            var acoesResultantes = acao.AplicarRegra(this);

            HistoricoAcao.Push(acao);

            if (acao is Primaria)
                realizador.AcoesDisponiveis--;

            else if (acao is Resultante)
                _resultantesPendentes.Remove((Resultante)acao);

            acao.Turno = _turnoAtual;

            foreach (var acaoResultante in acoesResultantes)
            {
                _resultantesPendentes.Add(acaoResultante);

                yield return acaoResultante;
            }
        }

        public Tuple<Jogador, IEnumerable<Resultante>> MoverParaProximoTurno()
        {
            if (JogadorAtual.AcoesDisponiveis > 0)
                throw new Exception("O jogador atual ainda possui ações disponíveis.");

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
                throw new Exception("Mesa já em duelo.");

            EmDuelo = true;
            Duelistas = new Tuple<Jogador, Jogador>(realizador, alvo);
        }

        public void SairModoDuelo()
        {
            if (!EmDuelo)
                throw new Exception("Mesa não está em duelo.");

            // TODO: Verificar se todas as ações resposta de duelo foram realizadas?

            EmDuelo = false;
            Duelistas = null;
        }

        public void Finalizar(Jogador vencedor) => DataHoraFim = DateTime.UtcNow;

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
                    throw new Exception($"Não é a vez do jogador \"{realizador}\" jogar.");
            }
        }

        private void _verificarResultantePendente(Acao acao)
        {
            if (acao is Resultante)
            {
                if (!_resultantesPendentes.Contains(acao))
                    throw new Exception($"Resultante \"{acao}\" não esperada.");
            }
        }
    }
}
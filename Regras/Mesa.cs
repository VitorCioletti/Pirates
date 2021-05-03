namespace ServidorPiratas.Regras
{
    using Acoes;
    using Baralhos;
    using System.Collections.Generic;
    using System;

    public class Mesa
    {
        public string Id { get; private set; }

        public List<Jogador> Jogadores { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public Jogador JogadorAtual { get; set; }

        public bool EmDuelo { get; set; }

        public Tuple<Jogador, Jogador> Duelistas { get; set; }

        public Queue<Jogador> OrdemDeJogadores { get; private set; }

        public Central BaralhoCentral { get; private set; }

        public Descarte BaralhoDescarte { get; set; }
        
        public Stack<Acao> HistoricoAcao { get; private set; }

        public Mesa(List<Jogador> jogadores)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new Central();
            BaralhoDescarte = new Descarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _geraOrdemDeJogadores(jogadores);
        }

        public void ProcessaAcao(Acao Acao)
        {
            var realizador = Acao.Realizador;

            if (realizador == JogadorAtual)
            {
                Acao.AplicaRegra(this);

                HistoricoAcao.Push(Acao);
            }
            else
                throw new Exception($"Não é a vez do jogador \"{realizador}\" jogar.");
        }

        public Jogador ObtemProximoJogador() 
        {
            var proximoJogador = OrdemDeJogadores.Dequeue();
            OrdemDeJogadores.Enqueue(proximoJogador);

            return proximoJogador;
        }

        public void Finaliza() => throw new NotImplementedException();

        private Queue<Jogador> _geraOrdemDeJogadores(List<Jogador> jogadores) => new Queue<Jogador>(jogadores);
    }
}
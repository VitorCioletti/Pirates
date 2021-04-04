namespace ServidorPiratas.Entidades
{
    using Baralhos.Tipos;
    using Jogadas;
    using System.Collections.Generic;
    using System;

    public class Mesa
    {
        public string Id { get; private set; }

        public List<Jogador> Jogadores { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public Jogador JogadorAtual { get; set; }

        public Queue<Jogador> OrdemDeJogadores { get; private set; }

        public Central BaralhoCentral { get; private set; }

        private Descarte BaralhoDescarte { get; set; }
        
        public Stack<Jogada> HistoricoJogadas { get; private set; }

        public Mesa(List<Jogador> jogadores)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new Central();
            BaralhoDescarte = new Descarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _geraOrdemDeJogadores(jogadores);
        }

        public void ProcessaJogada(Jogada jogada)
        {
            var jogador = jogada.Jogador;

            if (jogador == JogadorAtual)
            {
                jogada.AplicarRegra(this);

                HistoricoJogadas.Push(jogada);
            }
            else
                throw new Exception($"Não é a vez do jogador \"{jogador}\" jogar.");
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
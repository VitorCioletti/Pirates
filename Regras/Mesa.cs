namespace ServidorPiratas.Regras
{
    using Acoes;
    using Acoes.Tipos;
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

        public BaralhoCentral BaralhoCentral { get; private set; }

        public PilhaDescarte PilhaDescarte { get; set; }
        
        public Stack<Acao> HistoricoAcao { get; private set; }

        public Mesa(List<Jogador> jogadores)
        {
            Id = Guid.NewGuid().ToString();
            DataHoraInicio = DateTime.UtcNow;

            BaralhoCentral = new BaralhoCentral();
            PilhaDescarte = new PilhaDescarte();

            Jogadores = jogadores;
            OrdemDeJogadores = _geraOrdemDeJogadores(jogadores);
        }

        public void ProcessaAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            if (realizador == JogadorAtual)
            {
                acao.AplicarRegra(this);

                HistoricoAcao.Push(acao);

                if (acao.GetType() == typeof(Primaria))
                    realizador.AcoesDisponiveis--;
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
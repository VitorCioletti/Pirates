namespace ServidorPiratas.Regras
{
    using Acoes;
    using Acoes.Tipos;
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

        public bool EmDuelo { get; private set; }

        public Tuple<Jogador, Jogador> Duelistas { get; private set; }

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

        public Resultante ProcessaAcao(Acao acao)
        {
            var realizador = acao.Realizador;

            if (realizador == JogadorAtual)
            {
                var acaoResultante = acao.AplicarRegra(this);

                HistoricoAcao.Push(acao);

                if (acao.GetType() == typeof(Primaria))
                    realizador.AcoesDisponiveis--;

                return acaoResultante;
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

            EmDuelo = false;
            Duelistas = null;
        }

        public void Finaliza(Jogador vencedor) => throw new NotImplementedException();

        private Queue<Jogador> _geraOrdemDeJogadores(List<Jogador> jogadores) => new Queue<Jogador>(jogadores);
    }
}
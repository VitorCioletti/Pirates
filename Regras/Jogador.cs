namespace ServidorPiratas.Regras
{
    using Acoes.Primarias;
    using Cartas.Tipos;
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class Jogador 
    {
        public string Id { get; private set; }

        public int AcoesDisponiveis { get; set; }

        public Mao Mao { get; set; } 

        public Embarcacao Embarcacao { get; set; }

        public Duelo Canhao { get ; set; }

        public List<Tripulacao> Tripulacao { get; set; }

        public Jogador(string id)
        {
            Id = id;
            Mao = new Mao(new List<Carta>());
        }

        public DescerCarta DescerCarta(Carta carta) => new DescerCarta(this, carta);

        public ComprarCarta ComprarCarta() => new ComprarCarta(this);

        public Duelar IniciarDuelo(Jogador jogadorAtacado) => new Duelar(this, jogadorAtacado);

        public int CalcularPontosDuelo()
        {
            var pontosDuelo = Tripulacao.Sum(t => t.Tiros);

            pontosDuelo += Canhao != null ? Canhao.Tiros : 0;

            return pontosDuelo;
        }

        public override string ToString() => Id;

        public override bool Equals(object obj) => obj is Jogador jogador && this == jogador;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Jogador jogador1, Jogador jogador2) => jogador1.Id == jogador2.Id;

        public static bool operator !=(Jogador jogador1, Jogador jogador2) => jogador1.Id != jogador2.Id;
    }
}
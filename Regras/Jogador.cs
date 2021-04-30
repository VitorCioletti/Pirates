namespace ServidorPiratas.Regras
{
    using Acoes.Tipos.Jogador;
    using Cartas.Tipos;
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class Jogador 
    {
        public string Id { get; private set; }

        public int AcaoDisponiveis { get; set; }

        public List<Carta> Mao { get; set; } 

        public List<Tripulacao> Tripulacao { get; set; }

        public Jogador(string id)
        {
            Id = id;
            Mao = new List<Carta>();
        }

        public DescerCarta DescerCarta(Carta carta)
        {
            if (!Mao.Contains(carta))
                throw new Exception($"Jogador \"{Id}\" não possui carta \"{carta.Nome}\".");

            return new DescerCarta(this, carta); 
        }

        public ComprarCarta ComprarCarta() 
        {
            var quantidadeMaximaCartas = 10;

            if (Mao.Count >= quantidadeMaximaCartas)
                throw new Exception("Limite de cartas na mão atingido.");

            return new ComprarCarta(this);
        }

        public Duelar IniciarDuelo(Jogador jogadorAtacado) => new Duelar(this, jogadorAtacado);

        public int CalculaPontosDuelo() => Tripulacao.Sum(t => t.Tiros);

        public override string ToString() => Id;

        public override bool Equals(object obj) => obj is Jogador jogador && this == jogador;

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Jogador jogador1, Jogador jogador2) => jogador1.Id == jogador2.Id;

        public static bool operator !=(Jogador jogador1, Jogador jogador2) => jogador1.Id != jogador2.Id;
    }
}
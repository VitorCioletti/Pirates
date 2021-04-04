namespace ServidorPiratas.Entidades
{
    using Cartas.Tipos.Tripulacao;
    using Cartas;
    using Jogadas.Tipos;
    using System.Collections.Generic;

    public class Jogador 
    {
        public string Id { get; private set; }

        public int JogadasDisponiveis { get; set; }

        public List<Carta> CartasNaMao { get; set; } 

        public List<Tripulacao> Tripulacao { get; set; }

        public Jogador(string id)
        {
            Id = id;
            CartasNaMao = new List<Carta>();
        }
        
        public DescerCarta DescerCarta(Carta carta) => new DescerCarta(this, carta); 

        public ComprarCarta ComprarCarta() => new ComprarCarta(this);

        public IniciarDuelo IniciarDuelo(Jogador jogadorAtacado) => new IniciarDuelo(this, jogadorAtacado);

        public DeclararVitoria DeclararVitoria() => new DeclararVitoria(this);

        public override string ToString() => Id;

        public override bool Equals(object obj) => obj is Jogador jogador && this == jogador; 

        public static bool operator ==(Jogador jogador1, Jogador jogador2) => jogador1.Id == jogador2.Id;

        public static bool operator !=(Jogador jogador1, Jogador jogador2) => jogador1.Id != jogador2.Id;
    }
}
namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    using Entidades.Cartas;

    public class DescerCarta : Jogada
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta) : base(jogador)
        {
            Carta = carta;
        }

        public override void AplicaRegra(Mesa mesa) => Carta.AplicaEfeito(this, mesa);
    }
}
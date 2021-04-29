namespace ServidorPiratas.Entidades.Acoes.Tipos
{
    using Entidades.Cartas;

    public class DescerCarta : Acao
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta) : base(jogador)
        {
            Carta = carta;
        }

        public override void AplicaRegra(Mesa mesa) 
        {
            Realizador.CartasNaMao.Remove(Carta);
            mesa.BaralhoDescarte.InsereTopo(Carta);

            Carta.AplicaEfeito(this, mesa);
        }
    }
}
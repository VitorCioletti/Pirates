namespace ServidorPiratas.Regras.Acoes.Tipos.Jogador
{
    using Regras.Cartas;
    using Regras;

    public class DescerCarta : Acao
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta) : base(jogador)
        {
            Carta = carta;
        }

        public override void AplicaRegra(Mesa mesa) 
        {
            Realizador.Mao.Remove(Carta);
            mesa.BaralhoDescarte.InsereTopo(Carta);

            Carta.AplicaEfeito(this, mesa);
        }
    }
}
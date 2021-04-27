namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    using System;
    using Cartas;

    public class DescartarCarta: Jogada
    {
        public string IdJogadaOrigem { get; private set; }

        public Carta CartaDescartada { get; private set; }

        public DescartarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            if (Alvo.CartasNaMao.Remove(CartaDescartada))
                mesa.BaralhoDescarte.InsereTopo(CartaDescartada);
            else
                throw new Exception(
                    $"Carta \"{CartaDescartada.Nome}\" não está na mão do jogador \"{Realizador.Id}\".");
        }
    }
}
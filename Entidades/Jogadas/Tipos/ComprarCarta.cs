namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    using System;

    public class ComprarCarta : Jogada
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicaRegra(Mesa mesa)
        {
            var quantidadeMaximaCartas = 10;
            var cartasNaMao = Realizador.CartasNaMao;

            if (cartasNaMao.Count < quantidadeMaximaCartas)
                cartasNaMao.Add(mesa.BaralhoCentral.ObtemTopo());
            else
                throw new Exception("Limite de cartas na mão atingido.");
        }
    }
}
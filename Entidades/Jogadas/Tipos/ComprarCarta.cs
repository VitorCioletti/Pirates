namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    using System;

    public class ComprarCarta : Jogada
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicaRegra(Mesa mesa) => Realizador.CartasNaMao.Add(mesa.BaralhoCentral.ObtemTopo());
    }
}
namespace ServidorPiratas.Entidades.Acoes.Tipos
{
    using System;

    public class ComprarCarta : Acao
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicaRegra(Mesa mesa) => Realizador.CartasNaMao.Add(mesa.BaralhoCentral.ObtemTopo());
    }
}
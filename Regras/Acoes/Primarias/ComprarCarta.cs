namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using Regras;
    using Tipos;

    public class ComprarCarta : Primarias
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicaRegra(Mesa mesa) => Realizador.Mao.Add(mesa.BaralhoCentral.ObtemTopo());
    }
}
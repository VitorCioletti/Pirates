namespace ServidorPiratas.Regras.Acoes.Tipos.Jogador
{
    using Regras;

    public class ComprarCarta : Acao
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicaRegra(Mesa mesa) => Realizador.Mao.Add(mesa.BaralhoCentral.ObtemTopo());
    }
}
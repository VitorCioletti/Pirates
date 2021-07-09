namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using Regras;
    using Tipos;

    public class ComprarCarta : Primaria
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override void AplicarRegra(Mesa mesa) => Realizador.Mao.Adicionar(mesa.BaralhoCentral.ObterTopo());
    }
}
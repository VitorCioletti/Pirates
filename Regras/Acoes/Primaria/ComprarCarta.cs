namespace ServidorPiratas.Regras.Acoes.Primaria
{
    using Regras;
    using Tipos;

    public class ComprarCarta : Primaria
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(mesa.BaralhoCentral.ObterTopo());

            return null;
        }
    }
}
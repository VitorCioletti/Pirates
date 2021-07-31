namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Regras;
    using Tipos;

    public class ComprarCarta : Resultante
    {
        public ComprarCarta(Jogador jogador) : base(jogador) { }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(mesa.BaralhoCentral.ObterTopo());

            return null;
        }
    }
}
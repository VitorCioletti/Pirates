namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using Tipos;

    public class DescartarCarta: Resultante
    {
        public Carta CartaDescartada { get; private set; }

        public DescartarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Alvo.Mao.Remover(CartaDescartada);
            mesa.PilhaDescarte.InserirTopo(CartaDescartada);

            return null;
        }
    }
}
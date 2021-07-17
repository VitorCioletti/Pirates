namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using Tipos;

    public class RoubarCarta: Resultante
    {
        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaRoubada);
            Alvo.Mao.Remover(CartaRoubada);

            return null;
        }
    }
}
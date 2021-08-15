namespace ServidorPiratas.Regras.Acoes.Resultante
{
    using Cartas;
    using Regras;
    using Tipos;

    public class RoubarCarta: Resultante
    {
        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaRoubada);
            Alvo.Mao.Remover(CartaRoubada);

            return null;
        }
    }
}
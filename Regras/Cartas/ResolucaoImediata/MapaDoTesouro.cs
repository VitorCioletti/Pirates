namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Tipos;

    public class MapaDoTesouro : ResolucaoImediata
    {
        private int _cartasObtidas = 4;

        public MapaDoTesouro(string nome) : base(nome) {}

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal Resultante _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasObtidas);

            return new EscolherCartaBaralho(acao.Realizador, baralhoCentral, cartasOpcoes);
        }
    }
}
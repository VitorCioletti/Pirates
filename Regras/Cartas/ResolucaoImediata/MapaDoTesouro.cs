namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Tipos;

    public class MapaDoTesouro : ResolucaoImediata
    {
        public MapaDoTesouro(string nome) : base(nome) {}

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal Resultante _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(4);

            return new EscolherCartaBaralho(acao.Realizador, baralhoCentral, cartasOpcoes);
        }
    }
}
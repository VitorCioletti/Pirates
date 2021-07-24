namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using Acoes.Tipos;
    using Tipos;

    public class Rum : ResolucaoImediata
    {
        public Rum(string nome) : base(nome) { }

        private int _cartasCompradas = 2;

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador.Mao, mesa.BaralhoCentral);

        internal Resultante _aplicarEfeito(Mao maoRealizador, BaralhoCentral baralhoCentral)
        {
            var cartasCompradas = baralhoCentral.ObterTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);

            return null;
        }
    }
}
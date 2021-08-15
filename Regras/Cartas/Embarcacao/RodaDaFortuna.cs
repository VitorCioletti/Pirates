namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;

    public class RodaDaFortuna : Embarcacao
    {
        private int _cartasAOlhar = 2;

        public RodaDaFortuna(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal Resultante _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasAOlhar);

            return new OlharCartasBaralho(acao, acao.Realizador, cartasOpcoes);
        }
    }
}
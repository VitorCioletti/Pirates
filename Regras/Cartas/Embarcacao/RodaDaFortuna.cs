namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class RodaDaFortuna : Embarcacao
    {
        private int _cartasAOlhar = 2;

        public RodaDaFortuna(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador, mesa.BaralhoCentral);

        internal Resultante _aplicarEfeito(Jogador realizador, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasAOlhar);

            return new OlharCartasBaralho(realizador, cartasOpcoes);
        }
    }
}
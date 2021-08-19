namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class RodaDaFortuna : Embarcacao
    {
        private int _cartasAOlhar = 2;

        public RodaDaFortuna(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasAOlhar);

            yield return new OlharCartasBaralho(acao, acao.Realizador, cartasOpcoes);
        }
    }
}
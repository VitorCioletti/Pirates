namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class RodaFortuna : BaseEmbarcacao
    {
        private readonly int _cartasAOlhar = 2;

        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            List<Carta> cartasOpcoes = baralhoCentral.ObterTopo(_cartasAOlhar);

            var olharCartasBaralho = new OlharCartasBaralho(baseAcao, baseAcao.Realizador, cartasOpcoes);
            var acoesResultantes = new List<BaseAcao> { olharCartasBaralho };

            return acoesResultantes;
        }
    }
}

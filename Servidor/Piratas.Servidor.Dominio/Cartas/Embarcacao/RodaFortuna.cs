namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;
    using Tipos;

    public class RodaFortuna : Embarcacao
    {
        private readonly int _cartasAOlhar = 2;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasAOlhar);

            var olharCartasBaralho = new OlharCartasBaralho(acao, acao.Realizador, cartasOpcoes);
            var acoesResultantes = new List<Acao> { olharCartasBaralho };

            return acoesResultantes;
        }
    }
}

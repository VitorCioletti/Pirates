namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Baralhos;

    public class Rum : BaseResolucaoImediata
    {
        private readonly int _cartasCompradas = 2;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Mao maoRealizador = acao.Realizador.Mao;
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            List<Carta> cartasCompradas = baralhoCentral.ObterTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);

            return null;
        }
    }
}

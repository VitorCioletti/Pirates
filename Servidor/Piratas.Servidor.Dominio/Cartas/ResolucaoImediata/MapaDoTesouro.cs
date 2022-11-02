namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;

    public class MapaDoTesouro : BaseResolucaoImediata
    {
        private const int _cartasObtidas = 4;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            List<Carta> cartasOpcoes = baralhoCentral.ObterTopo(_cartasObtidas);

            var escolherCartaBaralho = new EscolherCartaBaralho(
                acao,
                acao.Realizador,
                baralhoCentral,
                cartasOpcoes);

            var acoesResultantes = new List<Acao> { escolherCartaBaralho };

            return acoesResultantes;
        }
    }
}

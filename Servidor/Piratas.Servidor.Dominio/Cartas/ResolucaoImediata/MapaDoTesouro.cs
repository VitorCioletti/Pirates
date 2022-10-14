namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;
    using Tipos;

    public class MapaDoTesouro : ResolucaoImediata
    {
        private readonly int _cartasObtidas = 4;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasObtidas);

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

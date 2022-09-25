namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Baralhos.Tipos;
    using Tipos;

    public class MapaDoTesouro : ResolucaoImediata
    {
        private readonly int _cartasObtidas = 4;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasObtidas);

            yield return new EscolherCartaBaralho(acao, acao.Realizador, baralhoCentral, cartasOpcoes);
        }
    }
}

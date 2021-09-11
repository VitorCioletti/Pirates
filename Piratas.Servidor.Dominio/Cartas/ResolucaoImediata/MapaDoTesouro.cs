namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using System.Collections.Generic;
    using Tipos;

    public class MapaDoTesouro : ResolucaoImediata
    {
        private int _cartasObtidas = 4;

        public MapaDoTesouro(string nome) : base(nome) {}

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.BaralhoCentral);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, BaralhoCentral baralhoCentral)
        {
            var cartasOpcoes = baralhoCentral.ObterTopo(_cartasObtidas);

            yield return new EscolherCartaBaralho(acao, acao.Realizador, baralhoCentral, cartasOpcoes);
        }
    }
}
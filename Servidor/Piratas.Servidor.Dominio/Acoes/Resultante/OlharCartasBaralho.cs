namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Baralhos;
    using Base;
    using Cartas;
    using Enums;

    public class OlharCartasBaralho : BaseResultanteComEscolhaBooleana
    {
        private List<Carta> _cartasOpcoes { get; set; }

        public OlharCartasBaralho(Acao origem, Jogador realizador, List<Carta> cartasOpcoes)
            : base(origem, realizador, TipoEscolha.Carta)
        {
            _cartasOpcoes = cartasOpcoes;
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            BaralhoCentral baralhoCentral = mesa.BaralhoCentral;

            if (EscolhaBooleana)
                baralhoCentral.InserirTopo(_cartasOpcoes);
            else
                baralhoCentral.InserirFundo(_cartasOpcoes);

            return null;
        }
    }
}

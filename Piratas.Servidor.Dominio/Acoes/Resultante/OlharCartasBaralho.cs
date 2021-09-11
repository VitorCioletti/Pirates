namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasBaralho : Resultante
    {
        public bool DevolverNoTopo { get; private set; }
    
        public List<Carta> CartasOpcoes { get; private set; }

        public OlharCartasBaralho(Acao origem, Jogador realizador, List<Carta> cartasOpcoes) : 
            base(origem, realizador) {}

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            var baralhoCentral = mesa.BaralhoCentral;

            if (DevolverNoTopo)
                baralhoCentral.InserirTopo(CartasOpcoes);
            else
                baralhoCentral.InserirFundo(CartasOpcoes);

            yield return null;
        }
    }
}
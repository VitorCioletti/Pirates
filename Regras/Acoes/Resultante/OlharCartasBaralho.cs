namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasBaralho : Resultante
    {
        public bool DevolverNoTopo { get; private set; }
    
        public List<Carta> CartasOpcoes { get; private set; }

        public OlharCartasBaralho(Acao origem, Jogador realizador, List<Carta> cartasOpcoes) : 
            base(origem, realizador) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            var baralhoCentral = mesa.BaralhoCentral;

            if (DevolverNoTopo)
                baralhoCentral.InserirTopo(CartasOpcoes);
            else
                baralhoCentral.InserirFundo(CartasOpcoes);

            return null;
        }
    }
}
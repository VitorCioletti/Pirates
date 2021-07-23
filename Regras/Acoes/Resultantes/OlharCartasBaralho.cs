namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasBaralho : Resultante
    {
        public bool DevolverNoTopo { get; private set; }
    
        public List<Carta> CartasOpcoes { get; private set; }

        public OlharCartasBaralho(Jogador realizador, List<Carta> cartasOpcoes) : base(realizador) {}

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
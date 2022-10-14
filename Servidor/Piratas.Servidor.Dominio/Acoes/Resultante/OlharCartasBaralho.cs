namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Cartas;
    using Tipos;

    public class OlharCartasBaralho : Resultante
    {
        public bool DevolverNoTopo { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public OlharCartasBaralho(
            Acao origem,
            Jogador realizador,
            List<Carta> cartasOpcoes) : base(origem, realizador) =>
            CartasOpcoes = cartasOpcoes;

        public override List<Acao> AplicarRegra(Mesa mesa)
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

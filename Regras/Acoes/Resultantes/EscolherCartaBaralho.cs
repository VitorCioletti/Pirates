namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class EscolherCartaBaralho : Resultante
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public EscolherCartaBaralho(Jogador realizador, List<Carta> cartasOpcoes) : base(realizador) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaEscolhida);

            CartasOpcoes.Remove(CartaEscolhida);
            mesa.BaralhoCentral.InserirFundo(CartasOpcoes);

            return null;
        }
    }
}
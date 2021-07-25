namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    // TODO : Fazer um EscolherCarta genérico?
    public class EscolherCartaPilhaDescarte<T> : Resultante where T : Carta
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public EscolherCartaPilhaDescarte(Jogador realizador, List<T> cartasOpcoes) : base(realizador) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaEscolhida);
            CartasOpcoes.Remove(CartaEscolhida);

            mesa.PilhaDescarte.InserirTopo(CartasOpcoes);
            mesa.PilhaDescarte.Embaralhar();

            return null;
        }
    }
}
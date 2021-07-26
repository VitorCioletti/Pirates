namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Baralhos;
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class EscolherCartaBaralho : Resultante
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public Baralho Baralho { get; private set; }

        public EscolherCartaBaralho(Jogador realizador, Baralho baralho, List<Carta> cartasOpcoes) : base(realizador)
        {
            Baralho = baralho;
            CartasOpcoes = cartasOpcoes;
        }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaEscolhida);

            CartasOpcoes.Remove(CartaEscolhida);
            Baralho.InserirFundo(CartasOpcoes);

            return null;
        }
    }
}
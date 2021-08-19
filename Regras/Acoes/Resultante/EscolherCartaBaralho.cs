namespace Piratas.Servidor.Regras.Acoes.Resultante
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

        public EscolherCartaBaralho(Acao origem, Jogador realizador, Baralho baralho, List<Carta> cartasOpcoes) : 
            base(origem, realizador)
        {
            Baralho = baralho;
            CartasOpcoes = cartasOpcoes;
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaEscolhida);

            CartasOpcoes.Remove(CartaEscolhida);
            Baralho.InserirFundo(CartasOpcoes);

            yield return null;
        }
    }
}
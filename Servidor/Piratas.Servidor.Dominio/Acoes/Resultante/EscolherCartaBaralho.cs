namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Baralhos;
    using Cartas;
    using Tipos;

    public class EscolherCartaBaralho : Resultante
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public Baralho Baralho { get; private set; }

        public EscolherCartaBaralho(
            Acao origem,
            Jogador realizador,
            Baralho baralho,
            List<Carta> cartasOpcoes) : base(origem, realizador)
        {
            Baralho = baralho;
            CartasOpcoes = cartasOpcoes;
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaEscolhida);

            CartasOpcoes.Remove(CartaEscolhida);
            Baralho.InserirFundo(CartasOpcoes);

            return null;
        }
    }
}

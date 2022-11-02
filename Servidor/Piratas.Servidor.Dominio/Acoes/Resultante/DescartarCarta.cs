namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Tesouro;
    using Enums;
    using Excecoes.Acoes;

    public class DescartarCarta : BaseResultanteComListaEscolhas
    {
        public DescartarCarta(
            Acao origem,
            Jogador realizador,
            Jogador alvo,
            List<string> cartasOpcoes)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartasOpcoes,
                alvo: alvo)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas[0];

            Carta cartaEscolhida = Alvo.Mao.ObterPorId(escolha);

            if (cartaEscolhida.GetType() == typeof(Tesouro))
                throw new ProibidoDescerCartaExcecao(this, cartaEscolhida);

            Alvo.Mao.Remover(cartaEscolhida);
            mesa.PilhaDescarte.InserirTopo(cartaEscolhida);

            return null;
        }
    }
}

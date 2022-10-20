namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Baralhos;
    using Cartas;
    using Base;
    using Enums;

    public class EscolherCartaBaralho : BaseResultanteComListaEscolhas
    {
        private Baralho _baralho;

        private List<Carta> _cartasOpcoes;

        public EscolherCartaBaralho(
            Acao origem,
            Jogador realizador,
            Baralho baralho,
            List<Carta> cartasOpcoes)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartasOpcoes.Select(c => c.Id).ToList())
        {
            _baralho = baralho;
            _cartasOpcoes = cartasOpcoes;
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            Carta cartaEscolhida = _cartasOpcoes.First(c => c.Id.ToString() == escolha);

            Realizador.Mao.Adicionar(cartaEscolhida);

            _cartasOpcoes.Remove(cartaEscolhida);
            _baralho.InserirFundo(_cartasOpcoes);

            return null;
        }
    }
}

namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Baralhos;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class EscolherCartaBaralho : BaseResultanteComListaEscolhas
    {
        private readonly Baralho _baralho;

        private readonly List<Carta> _cartasOpcoes;

        public EscolherCartaBaralho(
            Acao origem,
            Jogador realizador,
            Baralho baralho,
            List<Carta> cartasOpcoes)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartasOpcoes.ObterIds())
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

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
        private readonly BaseBaralho _baseBaralho;

        private readonly List<Carta> _cartasOpcoes;

        public EscolherCartaBaralho(
            BaseAcao origem,
            Jogador realizador,
            BaseBaralho baseBaralho,
            List<Carta> cartasOpcoes)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartasOpcoes.ObterIds())
        {
            _baseBaralho = baseBaralho;
            _cartasOpcoes = cartasOpcoes;
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            Carta cartaEscolhida = _cartasOpcoes.First(c => c.Id.ToString() == escolha);

            Realizador.Mao.Adicionar(cartaEscolhida);

            _cartasOpcoes.Remove(cartaEscolhida);
            _baseBaralho.InserirFundo(_cartasOpcoes);

            return null;
        }
    }
}

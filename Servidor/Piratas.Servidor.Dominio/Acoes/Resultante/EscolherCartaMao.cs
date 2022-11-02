namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Base;
    using Enums;

    public class EscolherCartaMao : BaseResultanteComListaEscolhas
    {
        private readonly Action<Carta> _aposEscolha;

        public EscolherCartaMao(
            BaseAcao origem,
            Jogador realizador,
            List<string> cartasOpcao,
            Action<Carta> aposEscolha)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                cartasOpcao)
        {
            _aposEscolha = aposEscolha;
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            Carta cartaEscolhida = Realizador.Mao.ObterPorId(escolha);

            _aposEscolha(cartaEscolhida);

            return null;
        }
    }
}

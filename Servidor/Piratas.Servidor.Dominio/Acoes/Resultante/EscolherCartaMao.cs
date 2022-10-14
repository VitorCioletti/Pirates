namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Cartas;
    using Excecoes.Acoes;
    using Tipos;

    public class EscolherCartaMao : Resultante
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcao { get; private set; }

        private readonly Action<Carta> _aposEscolha;

        public EscolherCartaMao(
            Acao origem,
            Jogador realizador,
            List<Carta> cartasOpcao,
            Action<Carta> aposEscolha) : base(origem, realizador)
        {
            CartasOpcao = cartasOpcao;
            _aposEscolha = aposEscolha;
        }

        public Func<Jogador, Resultante> ResultanteAposEscolha { get; private set; }

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            if (!CartasOpcao.Contains(CartaEscolhida))
                throw new CartaNaoEUmaOpcaoException(this, CartaEscolhida);

            _aposEscolha(CartaEscolhida);

            yield return null;
        }
    }
}

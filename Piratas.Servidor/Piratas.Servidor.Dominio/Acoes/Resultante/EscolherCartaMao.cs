namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using System.Collections.Generic;
    using System;
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

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (!CartasOpcao.Contains(CartaEscolhida))
                throw new ArgumentException($"Carta \"{CartaEscolhida.Id}\" não é uma opção.");

            _aposEscolha(CartaEscolhida);

            yield return null;
        }
    }
}

namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Cartas;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class EscolherCartaMao : Resultante
    {
        public Carta CartaEscolhida { get; private set; }

        public List<Carta> CartasOpcao { get; private set; }

        private Action<Carta> _aposEscolha;

        public EscolherCartaMao(Acao origem, Jogador realizador, List<Carta> cartasOpcao, 
            Action<Carta> aposEscolha) : base(origem, realizador)
        {
            CartasOpcao = cartasOpcao;
            _aposEscolha = aposEscolha;
        }

        public Func<Jogador, Resultante> ResultanteAposEscolha { get; private set; }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (!CartasOpcao.Contains(CartaEscolhida))
                throw new ArgumentException($"Carta \"{CartaEscolhida.Nome}\" não é uma opção.");
        
            _aposEscolha(CartaEscolhida);

            return null;
        }
    }
}
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Regras;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class EscolherJogador : Resultante
    {
        public Jogador JogadorEscolhido { get; private set; }

        public List<Jogador> JogadoresOpcao { get; private set; }

        public Func<Jogador, Resultante> ResultanteAposEscolha { get; private set; }

        public EscolherJogador(
            Jogador realizador, 
            List<Jogador> jogadoresOpcao, 
            Func<Jogador, Resultante> resultanteAposEscolha) : base(realizador) => 
            ResultanteAposEscolha = resultanteAposEscolha;

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (!JogadoresOpcao.Contains(JogadorEscolhido))
                throw new Exception($"Jogador \"{JogadorEscolhido.Id}\" não é uma opção.");

            return ResultanteAposEscolha(JogadorEscolhido); 
        }
    }
}
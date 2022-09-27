namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Excecoes.Acoes;
    using Tipos;

    public class EscolherJogador : Resultante
    {
        public Jogador JogadorEscolhido { get; private set; }

        public List<Jogador> JogadoresOpcao { get; private set; }

        public Func<Acao, Jogador, IEnumerable<Resultante>> ResultanteAposEscolha { get; private set; }

        public EscolherJogador(
            Acao origem,
            Jogador realizador,
            List<Jogador> jogadoresOpcao,
            Func<Acao, Jogador, IEnumerable<Resultante>> resultanteAposEscolha) : base(origem, realizador)
        {
            ResultanteAposEscolha = resultanteAposEscolha;
            JogadoresOpcao = jogadoresOpcao;
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (!JogadoresOpcao.Contains(JogadorEscolhido))
                throw new JogadorNaoEUmaOpcaoException(this, JogadorEscolhido);

            return ResultanteAposEscolha(this, JogadorEscolhido);
        }
    }
}

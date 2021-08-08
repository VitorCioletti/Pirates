namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Papagaio : ResolucaoImediata
    {
        public Papagaio(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(mesa.HistoricoAcao, mesa.ProcessarAcao);

        internal Resultante _aplicarEfeito(Stack<Acao> historicoAcao, Func<Acao, Resultante> executa)
        {
            // TODO: Só pode duplicar efeito se a ação anterior for DescerCarta?
            var ultimaAcao = historicoAcao.Peek();

            // TODO: Verificar se a última acao é primária e se foi o realizador que a fez.
            executa(ultimaAcao);

            return null;
        }
    }
}
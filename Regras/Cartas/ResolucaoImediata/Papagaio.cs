namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Papagaio : ResolucaoImediata
    {
        public Papagaio(string nome) : base(nome) { }

        public override void AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(mesa.HistoricoAcao, mesa.ProcessaAcao);

        internal void _aplicaEfeito(Stack<Acao> historicoAcao, Action<Acao> executa)
        {
            var ultimaAcao = historicoAcao.Peek();

            executa(ultimaAcao);
        }
    }
}
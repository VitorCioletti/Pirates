namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class HomemAoMar : ResolucaoImediata
    {
        public HomemAoMar(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Alvo.Tripulacao, mesa.PilhaDescarte);

        internal Resultante _aplicaEfeito(List<Tripulacao> tripulacao, PilhaDescarte pilhaDescarte)
        {
            var tripulacaoAfogada = tripulacao[0];
            tripulacao.RemoveAt(0);

            pilhaDescarte.InserirTopo(tripulacaoAfogada);

            return null;
        }
    }
}
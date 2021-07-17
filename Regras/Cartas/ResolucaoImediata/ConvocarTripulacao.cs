namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public ConvocarTripulacao(string nome) : base(nome) { }


        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador.Tripulacao, mesa.PilhaDescarte);

        internal Resultante _aplicaEfeito(List<Tripulacao> embarcacao, PilhaDescarte pilhaDescarte)
        {
            var tripulacao = pilhaDescarte.Obter<Tripulacao>();
            embarcacao.Add(tripulacao); // TODO: Criar uma classe para validar quantidade de itens?

            return null;
        }
    }
}
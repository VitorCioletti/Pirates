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
            _aplicaEfeito(acao.Realizador.Campo, mesa.PilhaDescarte);

        internal Resultante _aplicaEfeito(Campo campo , PilhaDescarte pilhaDescarte)
        {
            var tripulacao = pilhaDescarte.Obter<Tripulacao>();
            campo.Adicionar(tripulacao);

            return null;
        }
    }
}
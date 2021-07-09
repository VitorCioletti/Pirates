namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public ConvocarTripulacao(string nome) : base(nome) { }


        public override void AplicaEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador.Tripulacao, mesa.PilhaDescarte);

        internal void _aplicaEfeito(List<Tripulacao> embarcacao, PilhaDescarte pilhaDescarte)
        {
            var tripulacao = pilhaDescarte.Obter<Tripulacao>();
            embarcacao.Add(tripulacao); // TODO: Criar uma classe para validar quantidade de itens?
        }
    }
}
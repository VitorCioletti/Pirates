namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public ConvocarTripulacao(string nome) : base(nome) { }


        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador, mesa.PilhaDescarte);

        internal Resultante _aplicaEfeito(Jogador realizador, PilhaDescarte pilhaDescarte)
        {
            var tripulacoesDescartadas = pilhaDescarte.ObterTodas<Tripulacao>();
  
            return new EscolherCartaPilhaDescarte<Tripulacao>(realizador, tripulacoesDescartadas);
        }
    }
}
namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos;
    using Cartas.Tipos;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public ConvocarTripulacao(string nome) : base(nome) { }


        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador, mesa.PilhaDescarte);

        internal Resultante _aplicarEfeito(Jogador realizador, PilhaDescarte pilhaDescarte)
        {
            var tripulacoesDescartadas = pilhaDescarte.ObterTodas<Tripulacao>().ConvertAll<Carta>(t => (Carta)t);

            return new EscolherCartaBaralho(realizador, pilhaDescarte, tripulacoesDescartadas);
        }
    }
}
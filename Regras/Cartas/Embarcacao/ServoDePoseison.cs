namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;

    public class ServoDePoseidon : Embarcacao
    {
        public ServoDePoseidon(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal Resultante _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte) =>
            new EscolherCartaBaralho(acao, acao.Realizador, pilhaDescarte, pilhaDescarte.ObterTodas<Carta>());
    }
}
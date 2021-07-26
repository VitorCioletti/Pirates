namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos;
    using Cartas.Tipos;

    public class ServoDePoseidon : Embarcacao
    {
        public ServoDePoseidon(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador, mesa.PilhaDescarte);

        internal Resultante _aplicarEfeito(Jogador realizador, PilhaDescarte pilhaDescarte) =>
            new EscolherCartaBaralho(realizador, pilhaDescarte, pilhaDescarte.ObterTodas<Carta>());
    }
}
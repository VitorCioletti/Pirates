namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class HomemAoMar : ResolucaoImediata
    {
        public HomemAoMar(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador, acao.Alvo);

        internal Resultante _aplicarEfeito(Jogador realizador, Jogador alvo) => new AfogarTripulacao(realizador, alvo);
    }
}
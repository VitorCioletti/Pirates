namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class HomemAoMar : ResolucaoImediata
    {
        public HomemAoMar(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, acao.Alvo);

        internal Resultante _aplicarEfeito(Acao acao, Jogador alvo) => 
            new AfogarTripulacao(acao, acao.Realizador, alvo);
    }
}
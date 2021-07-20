namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public Luneta(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa _) => _aplicaEfeito(acao.Realizador, acao.Alvo);

        internal Resultante _aplicaEfeito(Jogador realizador, Jogador alvo) => new DescartarCarta(realizador, alvo);
    }
}
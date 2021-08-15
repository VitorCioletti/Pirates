namespace ServidorPiratas.Regras.Cartas.Passivo
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class BauArmadilha : ResolucaoImediata
    {
        public BauArmadilha(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao Acao, Mesa mesa) => null;
    }
}
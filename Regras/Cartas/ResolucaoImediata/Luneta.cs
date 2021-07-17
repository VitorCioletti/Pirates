namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public Luneta(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa _) => 
            _aplicaEfeito(null, acao.Alvo.Mao);

        internal Resultante _aplicaEfeito(Carta cartaDescartada, Mao maoAlvo)
        {
            maoAlvo.Remover(cartaDescartada);

            return null;
        }
    }
}
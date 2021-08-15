namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public Luneta(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa _) => _aplicarEfeito(acao);

        internal Resultante _aplicarEfeito(Acao acao) => new DescartarCarta(acao, acao.Realizador, acao.Alvo);
    }
}
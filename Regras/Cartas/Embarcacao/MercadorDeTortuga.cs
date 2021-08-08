namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;

    public class MercadorDeTortuga : Embarcacao
    {
        public MercadorDeTortuga(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao); 

        internal Resultante _aplicarEfeito(Acao acao) => new ComprarCarta(acao, acao.Realizador);
    }
}
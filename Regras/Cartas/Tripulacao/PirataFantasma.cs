namespace ServidorPiratas.Regras.Cartas.Tripulacao
{
    using Acoes;
    using Tipos;

    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome) => Tiros = 0;

        public override void AplicarEfeito(Acao acao, Mesa mesa) { }
    }
}
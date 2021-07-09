namespace ServidorPiratas.Regras.Cartas.Tripulacao
{
    using Acoes;
    using Tipos;

    public class PirataAmaldicoado : Tripulacao
    {
        public PirataAmaldicoado(string nome) : base(nome) => Tiros = -1;

        public override void AplicarEfeito(Acao acao, Mesa mesa) { }
    }
}
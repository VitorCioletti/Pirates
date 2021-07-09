namespace ServidorPiratas.Regras.Cartas.Tripulacao
{
    using Acoes;
    using Tipos;

    public class Pirata : Tripulacao
    {
        public Pirata(string nome) : base(nome) => Tiros = 1;

        public override void AplicaEfeito(Acao acao, Mesa mesa) { }
    }
}
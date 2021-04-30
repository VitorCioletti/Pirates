namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;

    public abstract class Tesouro : Carta
    {
        public Tesouro(string nome) : base(nome) { }

        public override void AplicaEfeito(Acao Acao, Mesa mesa)
        {
            throw new System.NotImplementedException();
        }
    }
}
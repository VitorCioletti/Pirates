namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;

    public abstract class Duelo : Carta
    {
        public int Tiros { get; protected set; }

        public Duelo(string nome) : base(nome) { }

        public override void AplicaEfeito(Acao Acao, Mesa mesa)
        {
            throw new System.NotImplementedException();
        }
    }
}
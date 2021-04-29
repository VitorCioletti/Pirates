namespace ServidorPiratas.Entidades.Cartas
{
    using Acoes;

    public abstract class Carta
    {
        public string Nome { get; private set; } 

        public Carta(string nome) => Nome = nome;

        public abstract void AplicaEfeito(Acao Acao, Mesa mesa);
    }
}
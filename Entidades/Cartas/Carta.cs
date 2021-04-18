namespace ServidorPiratas.Entidades.Cartas
{
    using Jogadas;

    public abstract class Carta
    {
        public string Nome { get; private set; } 

        public Carta(string nome) => Nome = nome;

        public abstract void AplicaEfeito(Jogada jogada, Mesa mesa);
    }
}
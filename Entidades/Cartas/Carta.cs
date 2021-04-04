namespace ServidorPiratas.Entidades.Cartas
{
    public abstract class Carta
    {
        public string Nome { get; private set; } 

        public Carta(string nome) => Nome = nome;
    }
}
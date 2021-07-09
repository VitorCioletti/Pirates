namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;

    public abstract class Tripulacao : Carta
    {
        public Tripulacao(string nome) : base(nome) { }

        public int Tiros { get; protected set; }
    }
}
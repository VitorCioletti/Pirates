namespace ServidorPiratas.Regras.Cartas.Tipos
{
    public abstract class Duelo : Carta
    {
        public int Tiros { get; protected set; }

        public Duelo(string nome) : base(nome) { }
    }
}
namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;

    public abstract class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        public Tesouro(string nome) : base(nome) { }
    }
}
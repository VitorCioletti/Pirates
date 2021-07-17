namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;
    using ServidorPiratas.Regras.Acoes.Tipos;

    public abstract class Tripulacao : Carta // TODO: Tripulação é uma carta ?
    {
        public Tripulacao(string nome) : base(nome) { }

        public int Tiros { get; protected set; }

        // TODO: Deveria possuir esse método?
        public override Resultante AplicarEfeito(Acao Acao, Mesa mesa)
        {
           return null; 
        }
    }
}
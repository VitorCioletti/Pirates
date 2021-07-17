namespace ServidorPiratas.Regras.Cartas
{
    using Acoes;
    using Acoes.Tipos;

    public abstract class Carta
    {
        public string Nome { get; private set; } 

        public Carta(string nome) => Nome = nome;

        public abstract Resultante AplicarEfeito(Acao Acao, Mesa mesa);
    }
}
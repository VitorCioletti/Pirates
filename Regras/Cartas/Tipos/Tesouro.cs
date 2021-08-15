namespace Piratas.Servidor.Regras.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;

    public class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        public Tesouro(string nome, int valor) : base(nome) => Valor = valor;

        public override Resultante AplicarEfeito(Acao Acao, Mesa mesa) => null;
    }
}
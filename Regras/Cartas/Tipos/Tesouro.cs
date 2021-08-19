namespace Piratas.Servidor.Regras.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;

    public class Tesouro : Carta
    {
        public int Valor { get; protected set; }

        public Tesouro(string nome, int valor) : base(nome) => Valor = valor;

        public override IEnumerable<Resultante> AplicarEfeito(Acao Acao, Mesa mesa) => null;
    }
}
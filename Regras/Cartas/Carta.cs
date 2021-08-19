namespace Piratas.Servidor.Regras.Cartas
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;

    public abstract class Carta
    {
        public string Nome { get; private set; } 

        public Carta(string nome) => Nome = nome;

        public abstract IEnumerable<Resultante> AplicarEfeito(Acao Acao, Mesa mesa);
    }
}
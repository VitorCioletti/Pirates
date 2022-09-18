namespace Piratas.Servidor.Dominio.Cartas
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;

    public abstract class Carta
    {
        public string Id { get; protected set; }

        public Carta() =>
            Id = this.GetType().ToString();

        public abstract IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa);
    }
}

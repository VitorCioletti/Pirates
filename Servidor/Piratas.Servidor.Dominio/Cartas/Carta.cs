namespace Piratas.Servidor.Dominio.Cartas
{
    using System.Collections.Generic;
    using Acoes;

    public abstract class Carta
    {
        public string Id { get; protected set; }

        protected Carta() => Id = this.GetType().ToString();

        public abstract List<Acao> AplicarEfeito(Acao acao, Mesa mesa);
    }
}

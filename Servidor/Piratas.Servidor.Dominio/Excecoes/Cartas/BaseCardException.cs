namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public abstract class BaseCardException : BaseDomainException
    {
        public Card Card { get; private set; }

        protected BaseCardException(Card card, string id, string message) : base(id, message)
        {
            Card = card;
        }
    }
}

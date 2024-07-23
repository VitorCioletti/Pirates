namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public abstract class BaseCardException : BaseDomainException
    {
        public Card Card { get; private set; }

        protected BaseCardException(Card card, string id, string message) : base(id, message)
        {
            Card = card;
        }
    }
}

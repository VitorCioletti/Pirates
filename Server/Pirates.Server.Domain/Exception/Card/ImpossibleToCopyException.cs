namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public class ImpossibleToCopyException : BaseCardException
    {
        public ImpossibleToCopyException(Card card, Card copiedCard)
            : base(card, "impossible-to-copy", $"It is not possible to copy card \"{copiedCard.Id}\".")
        {
        }
    }
}

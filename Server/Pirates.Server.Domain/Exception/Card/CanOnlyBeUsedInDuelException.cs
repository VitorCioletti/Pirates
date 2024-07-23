namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public class CanOnlyBeUsedInDuelException : BaseCardException
    {
        public CanOnlyBeUsedInDuelException(Card card)
            : base(
                card,
                "can-only-be-used-in-duel",
                $"Card \"{card.Id}\" can only be used in duel.")
        {
        }
    }
}

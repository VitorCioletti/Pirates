namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public class NoCrewMemberInDiscardDeckException : BaseCardException
    {
        public NoCrewMemberInDiscardDeckException(Card card)
            : base(card, "no-crew-member-in-discard-deck", "No crew member in discard deck.")
        {
        }
    }
}

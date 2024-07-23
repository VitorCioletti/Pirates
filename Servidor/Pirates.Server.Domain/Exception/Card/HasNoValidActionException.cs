namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public class HasNoValidActionException : BaseCardException
    {
        public HasNoValidActionException(Card card) : base(card, "has-no-valid-action", "Has no valid action.")
        {
        }
    }
}

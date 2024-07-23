namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;
    using Domain.Card;

    public class ForbiddenCardToStartDuelException : BaseActionException
    {
        public ForbiddenCardToStartDuelException(BaseAction action, Card card)
            : base(action, "forbidden-card-to-start-duel", $"Card \"{card.Id}\" is not allowed to start a duel.")
        {
        }
    }
}

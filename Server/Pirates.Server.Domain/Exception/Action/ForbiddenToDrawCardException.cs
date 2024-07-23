namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;
    using Domain.Card;

    public class ForbiddenToDrawCardException : BaseActionException
    {
        public ForbiddenToDrawCardException(BaseAction action, Card card)
            : base(action, "forbidden-to-draw-card", $"Forbidden to draw card of type \"{card.Id}\".")
        {
        }
    }
}

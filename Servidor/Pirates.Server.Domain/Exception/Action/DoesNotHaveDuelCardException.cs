namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class DoesNotHaveDuelCardException : BaseActionException
    {
        public DoesNotHaveDuelCardException(BaseAction action)
            : base(
                action,
                "does-not-have-duel-card",
                $"Player \"{action.Starter.Id}\" does not have any duel card.")
        {
        }
    }
}

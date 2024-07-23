namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class ChoiceLimitReachedException : BaseActionException
    {
        public ChoiceLimitReachedException(BaseAction action, int choicesAmount)
            : base(
                action,
                "reached-choice-limit",
                $"Choice limit to action \"{action}\" has been reached. Value \"{choicesAmount}\".")
        {
        }
    }
}

namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class ChoiceIsNotAnOptionException : BaseActionException
    {
        public ChoiceIsNotAnOptionException(BaseAction action, string choiceId) :
            base(action, "choice-is-not-an-option", $"Choice \"{choiceId}\" is not an option.")
        {
        }
    }
}

namespace Pirates.Server.Service.Exception.Match
{
    public class NotAvailableActionException : BaseMatchException
    {
        public string ActionId { get; private set; }

        public NotAvailableActionException(string actionId) :
            base("not-available-action", $"Action \"{actionId}\" not available.")
        {
            ActionId = actionId;
        }
    }
}

namespace Pirates.Server.Domain.Exception.Table
{
    public class HasAvailableActionsException : BaseTableException
    {
        public HasAvailableActionsException(Player player)
            : base("has-available-actions", $"Player \"{player.Id}\" still has available actions.")
        {
        }
    }
}

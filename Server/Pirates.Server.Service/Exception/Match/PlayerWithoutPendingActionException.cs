namespace Pirates.Server.Service.Exception.Match
{
    public class PlayerWithoutPendingActionException : BaseMatchException
    {
        public PlayerWithoutPendingActionException(string playerId)
            : base("player-without-pending-action", $"Player \"{playerId}\" without pending action.")
        {
        }
    }
}

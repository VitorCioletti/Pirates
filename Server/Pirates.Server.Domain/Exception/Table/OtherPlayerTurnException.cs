namespace Pirates.Server.Domain.Exception.Table
{
    public class OtherPlayerTurnException : BaseTableException
    {
        public OtherPlayerTurnException(Player player)
            : base("other-player-turn", $"Other player turn. Player \"{player.Id}\" tried to play.")
        {
        }
    }
}

namespace Pirates.Server.Service.Exception.Room
{
    public class PlayerIsNotInTheRoom : BaseRoomException
    {
        public PlayerIsNotInTheRoom(string playerId) :
            base("player-is-not-in-the-room", $"Player \"{playerId}\" is not in the room.")
        {
        }
    }
}

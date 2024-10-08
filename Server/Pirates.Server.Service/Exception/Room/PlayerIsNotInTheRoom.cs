namespace Pirates.Server.Service.Exception.Room
{
    public class PlayerIsNotInTheRoom : BaseRoomException
    {
        public PlayerIsNotInTheRoom(string idJogador) :
            base("player-is-not-in-the-room", $"Player \"{idJogador}\" is not in the room.")
        {
        }
    }
}

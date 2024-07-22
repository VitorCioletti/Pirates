namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    public class PlayerIsAlreadyInARoom : BaseRoomException
    {
        public PlayerIsAlreadyInARoom(string playerId) :
            base("player-is-already-in-a-room", $"Player \"{playerId}\" is in a room.")
        {
        }
    }
}

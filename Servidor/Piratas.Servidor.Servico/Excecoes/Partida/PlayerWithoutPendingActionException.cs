namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class PlayerWithoutPendingActionException : BaseMatchException
    {
        public PlayerWithoutPendingActionException(string playerId)
            : base("player-without-pending-action", $"Player \"{playerId}\" without pending action.")
        {
        }
    }
}

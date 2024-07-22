namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class HasAvailableActionsException : BaseTableException
    {
        public HasAvailableActionsException(Player player)
            : base("has-available-actions", $"Player \"{player.Id}\" still has available actions.")
        {
        }
    }
}

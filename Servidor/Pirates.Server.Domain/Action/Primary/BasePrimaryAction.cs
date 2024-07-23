namespace Pirates.Server.Domain.Action.Primary
{
    public abstract class BasePrimaryAction : BaseAction
    {
        protected BasePrimaryAction(Player starter, Player target = null) : base(starter, target)
        {
        }
    }
}

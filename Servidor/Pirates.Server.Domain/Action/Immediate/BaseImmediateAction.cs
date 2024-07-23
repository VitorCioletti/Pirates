namespace Pirates.Server.Domain.Action.Immediate
{
    public abstract class BaseImmediateAction : BaseAction
    {
        protected BaseImmediateAction(Player starter, Player target = null) : base(starter, target)
        {
        }
    }
}

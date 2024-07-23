namespace Pirates.Server.Domain.Exception.Table
{
    using Domain.Action.Resultant.Base;

    public class UnexpectedResultantAction : BaseTableException
    {
        public BaseResultant BaseResultant { get; private set; }

        public UnexpectedResultantAction(BaseResultant baseResultant)
            : base("unexpected-resultant-action", $"Unexpected \"{baseResultant.Id}\".")

        {
            BaseResultant = baseResultant;
        }
    }
}

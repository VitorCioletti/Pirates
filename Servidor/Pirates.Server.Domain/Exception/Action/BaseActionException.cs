namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public abstract class BaseActionException : BaseDomainException
    {
        public BaseAction Action { get; private set; }

        protected BaseActionException(BaseAction action, string id, string message) : base(id, message)
        {
            Action = action;
        }
    }
}

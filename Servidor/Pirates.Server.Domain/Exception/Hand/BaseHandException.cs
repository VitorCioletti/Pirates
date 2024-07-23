namespace Pirates.Server.Domain.Exception.Hand
{
    public abstract class BaseHandException : BaseDomainException
    {
        protected BaseHandException(string id, string message) : base(id, message)
        {
        }
    }
}

namespace Pirates.Server.Domain.Exception.Field
{
    public abstract class BaseFieldException : BaseDomainException
    {
        protected BaseFieldException(string id, string message) : base(id, message)
        {
        }
    }
}

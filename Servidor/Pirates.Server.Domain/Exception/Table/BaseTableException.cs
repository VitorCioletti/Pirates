namespace Pirates.Server.Domain.Exception.Table
{
    public abstract class BaseTableException : BaseDomainException
    {
        protected BaseTableException(string id, string message) : base(id, message)
        {
        }
    }
}

namespace Pirates.Server.Service.Exception.Match
{
    public abstract class BaseMatchException : BaseServiceException
    {
        protected BaseMatchException(string id, string message) : base(id, message)
        {
        }
    }
}

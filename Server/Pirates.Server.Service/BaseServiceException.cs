namespace Pirates.Server.Service;

public abstract class BaseServiceException : System.Exception
{
    public string Id { get; private set; }

    protected BaseServiceException(string id, string message) : base(message)
    {
        Id = id;
    }
}

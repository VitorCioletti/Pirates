namespace Pirates.Server.Service.Exception.Room
{
    public abstract class BaseRoomException : BaseServiceException
    {
        protected BaseRoomException(string id, string message) : base(id, message)
        {
        }
    }
}

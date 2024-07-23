namespace Pirates.Server.Service.Exception.Match
{
    public class MessageTypeNotSupported : BaseServiceException
    {
        public MessageTypeNotSupported() :
            base("message-type-not-supported", "Message type not supported.")
        {
        }
    }
}

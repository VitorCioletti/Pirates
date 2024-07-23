namespace Pirates.Server.Domain.Exception.Field
{
    public class NoShipException : BaseFieldException
    {
        public NoShipException() : base("no-ship", "There is not ship.")
        {
        }
    }
}

namespace Pirates.Server.Domain.Exception.Table
{
    public class ImmediateAlreadyRegisteredException : BaseTableException
    {
        public ImmediateAlreadyRegisteredException() : base(
            "immediate-already-registered",
            "There is an immediate action already registered.")
        {
        }
    }
}

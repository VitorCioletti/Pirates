namespace Pirates.Server.Domain.Exception.Table
{
    public class NoDuelException : BaseTableException
    {
        public NoDuelException() : base("no-duel", "There is no duel happening.")
        {
        }
    }
}

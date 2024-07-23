namespace Pirates.Server.Domain.Exception.Table
{
    public class InDuelException : BaseTableException
    {
        public InDuelException() : base("in-duel", "Table already in duel.")
        {
        }
    }
}

namespace Pirates.Server.Domain.Exception.Hand
{
    public class EmptyHandException : BaseHandException
    {
        public EmptyHandException() : base("empty-hand", "Hand is empty.")
        {
        }
    }
}

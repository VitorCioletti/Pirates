namespace Pirates.Server.Domain.Exception.Hand
{
    public class HandCardLimitReachedException : BaseHandException
    {
        public HandCardLimitReachedException() : base("hand-card-limit-reached", "Card limit reached.")
        {
        }
    }
}

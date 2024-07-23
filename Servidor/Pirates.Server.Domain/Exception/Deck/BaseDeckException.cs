namespace Pirates.Server.Domain.Exception.Deck
{
    public class BaseDeckException : BaseDomainException
    {
        public BaseDeckException(string id, string message) : base(id, message)
        {
        }
    }
}

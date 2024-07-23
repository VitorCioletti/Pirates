namespace Pirates.Server.Domain.Exception.Hand
{
    using Domain.Card;

    public class CardDoesNotExistInHandException : BaseHandException
    {
        public CardDoesNotExistInHandException(Card card)
            : base("card-does-not-exist-in-hand", $"Card \"{card.Id}\" does not exist in the hand.")
        {
        }
    }
}

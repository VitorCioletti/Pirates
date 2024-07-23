namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card;

    public class FullCrewException : BaseCardException
    {
        public FullCrewException(Card card, Player player) : base(
            card,
            "full-crew",
            $"Player \"{player.Id}\" crew is full.")
        {
        }
    }
}

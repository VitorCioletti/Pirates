namespace Pirates.Server.Domain.Deck
{
    using System.Collections.Generic;
    using System.Linq;
    using Card;
    using Exception.Deck;

    public class DiscardDeck : BaseDeck
    {
        public DiscardDeck() => Cards = [];

        public List<T> GetAll<T>() where T : Card
        {
            List<T> cards = Cards.OfType<T>().ToList();

            if (cards.Count == 0)
                throw new CardNotFoundInDiscardDeckException(typeof(T).ToString());

            return cards;
        }
    }
}

namespace Pirates.Server.Domain.Deck
{
    using System.Collections.Generic;
    using System.Linq;
    using Card;
    using Exception.Deck;

    public class DiscardDeck : BaseDeck
    {
        public DiscardDeck() => Cards = new LinkedList<Card>();

        public List<T> GetAll<T>() where T : Card
        {
            var cards = (List<T>)Cards.Select(c => c is T);

            if (cards.Count == 0)
                throw new CardNotFoundInDiscardDeckException(typeof(T).ToString());

            return cards;
        }
    }
}

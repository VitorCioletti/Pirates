namespace Pirates.Server.Domain.Deck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Card;

    public abstract class BaseDeck
    {
        public int CardsAmount => Cards.Count;

        protected LinkedList<Card> Cards { get; set; }

        public void PushTop(Card card) => PushTop(new List<Card> {card});

        public void PushTop(List<Card> cards) => _insert(cards, true);

        public void PushBottom(List<Card> cards) => _insert(cards, false);

        protected IEnumerable<Card> Shuffle(IEnumerable<Card> cards)
        {
            var random = new Random();

            List<Card> shuffledCards = cards.OrderBy(_ => random.Next()).ToList();

            return shuffledCards;
        }

        private void _insert(List<Card> cards, bool top)
        {
            foreach (Card card in cards)
            {
                if (top)
                    Cards.AddFirst(card);
                else
                    Cards.AddLast(card);
            }
        }
    }
}

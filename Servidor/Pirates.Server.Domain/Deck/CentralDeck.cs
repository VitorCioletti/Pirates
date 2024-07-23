namespace Pirates.Server.Domain.Deck
{
    using System.Collections.Generic;
    using Card;

    public class CentralDeck : BaseDeck
    {
        public void GenerateCards()
        {
            List<Card> newCards = CardsGenerator.Generate();

            IEnumerable<Card> shuffledCards = Shuffle(newCards);

            Cards = new LinkedList<Card>(shuffledCards);
        }

        public Card GetTop()
        {
            LinkedListNode<Card> lastNode = Cards.Last;

            if (lastNode == null)
                return null;

            Cards.RemoveLast();

            return lastNode.Value;
        }

        public List<Card> GetTop(int amount)
        {
            var cards = new List<Card>();

            for (int i = 0; i < amount; i++)
                cards.Add(GetTop());

            return cards;
        }
    }
}

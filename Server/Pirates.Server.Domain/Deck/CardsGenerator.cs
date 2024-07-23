namespace Pirates.Server.Domain.Deck;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Card;

public static class CardsGenerator
{
    private static List<Tuple<string, int>> _cardsConfiguration;

    public static void Configure(List<Tuple<string, int>> cardsConfiguration)
    {
        _cardsConfiguration = cardsConfiguration;
    }

    public static List<Card> Generate()
    {
        var cards = new List<Card>();

        foreach ((string name, int amount) in _cardsConfiguration)
        {
            IEnumerable<Card> newCards = _createCards(name, amount);

            cards.AddRange(newCards);
        }

        return cards;
    }

    private static IEnumerable<Card> _createCards(string name, int amount)
    {
        var cards = new List<Card>();

        if (amount == 0)
            return cards;

        for (int i = 0; i < amount; i++)
        {
            Card card = _create(name);

            cards.Add(card);
        }

        return cards;
    }

    private static Card _create(string name)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();

        Type cardType = executingAssembly.GetTypes().FirstOrDefault(t => t.Name == name);

        if (cardType is null)
            throw new InvalidOperationException($"Card \"{name}\" not found.");

        var card = (Card)Activator.CreateInstance(cardType);

        return card;
    }
}

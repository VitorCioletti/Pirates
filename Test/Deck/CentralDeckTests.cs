namespace Pirates.Server.Domain.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Deck;
using Domain.Card.ImmediateResolution;
using NUnit.Framework;

public class CentralDeckTests
{
    private CentralDeck _centralDeck;

    [SetUp]
    public void SetUp()
    {
        _centralDeck = new CentralDeck();
    }

    [Test]
    public void GenerateCardsMustPopulateTheDeckWithEveryConfiguredCard()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 2), new(nameof(Sake), 3)};

        CardsGenerator.Configure(cardsConfiguration);

        _centralDeck.GenerateCards();

        Assert.That(_centralDeck.CardsAmount, Is.EqualTo(5));
    }

    [Test]
    public void GenerateCardsMustShuffleWithoutLosingOrDuplicatingAnyCard()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 2), new(nameof(Sake), 3)};

        CardsGenerator.Configure(cardsConfiguration);

        _centralDeck.GenerateCards();

        List<Domain.Card.Card> drawnCards = _centralDeck.GetTop(5);

        Assert.That(drawnCards.OfType<Rum>().Count(), Is.EqualTo(2));
        Assert.That(drawnCards.OfType<Sake>().Count(), Is.EqualTo(3));
    }

    [Test]
    public void GetTopMustReturnNullWhenTheDeckIsEmpty()
    {
        CardsGenerator.Configure(new List<Tuple<string, int>>());

        _centralDeck.GenerateCards();

        Assert.That(_centralDeck.GetTop(), Is.Null);
    }

    [Test]
    public void GetTopMustRemoveTheReturnedCardFromTheDeck()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 1)]);

        _centralDeck.GenerateCards();

        Domain.Card.Card card = _centralDeck.GetTop();

        Assert.That(card, Is.Not.Null);
        Assert.That(_centralDeck.CardsAmount, Is.EqualTo(0));
        Assert.That(_centralDeck.GetTop(), Is.Null);
    }

    [Test]
    public void GetTopWithAmountMustReturnExactlyThatManyCardsWhenEnoughAreAvailable()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 3)]);

        _centralDeck.GenerateCards();

        List<Domain.Card.Card> cards = _centralDeck.GetTop(2);

        Assert.That(cards.Count, Is.EqualTo(2));
        Assert.That(cards, Has.None.Null);
        Assert.That(_centralDeck.CardsAmount, Is.EqualTo(1));
    }

    [Test]
    public void GetTopWithAmountMustPadWithNullsWhenTheDeckRunsOut()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 1)]);

        _centralDeck.GenerateCards();

        List<Domain.Card.Card> cards = _centralDeck.GetTop(3);

        Assert.That(cards.Count, Is.EqualTo(3));
        Assert.That(cards[0], Is.Not.Null);
        Assert.That(cards[1], Is.Null);
        Assert.That(cards[2], Is.Null);
    }
}

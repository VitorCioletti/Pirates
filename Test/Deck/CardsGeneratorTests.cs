namespace Pirates.Server.Domain.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Deck;
using Domain.Card.ImmediateResolution;
using NUnit.Framework;

public class CardsGeneratorTests
{
    [Test]
    public void GenerateMustCreateTheConfiguredAmountOfEachCard()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 2), new Tuple<string, int>(nameof(Sake), 3)]);

        List<Domain.Card.Card> cards = CardsGenerator.Generate();

        Assert.That(cards.Count, Is.EqualTo(5));
        Assert.That(cards.OfType<Rum>().Count(), Is.EqualTo(2));
        Assert.That(cards.OfType<Sake>().Count(), Is.EqualTo(3));
    }

    [Test]
    public void GenerateMustCreateDistinctInstancesForEachRequestedCard()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 2)]);

        List<Domain.Card.Card> cards = CardsGenerator.Generate();

        Assert.That(cards[0], Is.Not.SameAs(cards[1]));
    }

    [Test]
    public void GenerateMustSkipEntriesWithZeroAmount()
    {
        CardsGenerator.Configure([new Tuple<string, int>(nameof(Rum), 0)]);

        List<Domain.Card.Card> cards = CardsGenerator.Generate();

        Assert.That(cards, Is.Empty);
    }

    [Test]
    public void GenerateMustThrowInvalidOperationExceptionForAnUnknownCardName()
    {
        CardsGenerator.Configure([new Tuple<string, int>("NotARealCard", 1)]);

        Assert.Throws<InvalidOperationException>(() => CardsGenerator.Generate());
    }
}

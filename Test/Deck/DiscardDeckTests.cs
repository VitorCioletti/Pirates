namespace Pirates.Server.Domain.Test;

using System.Collections.Generic;
using Deck;
using Domain.Card.ImmediateResolution;
using Domain.Exception.Deck;
using NSubstitute;
using NUnit.Framework;

public class DiscardDeckTests
{
    private DiscardDeck _discardDeck;

    [SetUp]
    public void SetUp()
    {
        _discardDeck = new DiscardDeck();
    }

    [Test]
    public void NewDiscardDeckMustBeEmpty()
    {
        Assert.That(_discardDeck.CardsAmount, Is.EqualTo(0));
    }

    [Test]
    public void PushTopMustAddTheCard()
    {
        var card = Substitute.For<Domain.Card.Card>();

        _discardDeck.PushTop(card);

        Assert.That(_discardDeck.CardsAmount, Is.EqualTo(1));
    }

    [Test]
    public void GetAllMustThrowCardNotFoundInDiscardDeckExceptionWhenDeckIsEmpty()
    {
        Assert.Throws<CardNotFoundInDiscardDeckException>(() => _discardDeck.GetAll<Domain.Card.Card>());
    }

    [Test]
    public void GetAllMustThrowCardNotFoundInDiscardDeckExceptionWhenNoCardMatchesTheType()
    {
        _discardDeck.PushTop(Substitute.For<Domain.Card.Card>());

        Assert.Throws<CardNotFoundInDiscardDeckException>(() => _discardDeck.GetAll<Rum>());
    }

    [Test]
    public void GetAllMustReturnOnlyTheCardsMatchingTheRequestedType()
    {
        var rum = new Rum();
        var otherCard = Substitute.For<Domain.Card.Card>();

        _discardDeck.PushTop([rum, otherCard]);

        List<Rum> matchingCards = _discardDeck.GetAll<Rum>();

        Assert.That(matchingCards.Count, Is.EqualTo(1));
        Assert.That(matchingCards[0], Is.EqualTo(rum));
    }

    [Test]
    public void GetAllMustReturnEveryCardOfTheRequestedTypeWhenThereAreMultiple()
    {
        var firstRum = new Rum();
        var secondRum = new Rum();

        _discardDeck.PushTop([firstRum, secondRum]);

        List<Rum> matchingCards = _discardDeck.GetAll<Rum>();

        Assert.That(matchingCards.Count, Is.EqualTo(2));
        Assert.That(matchingCards, Does.Contain(firstRum));
        Assert.That(matchingCards, Does.Contain(secondRum));
    }
}

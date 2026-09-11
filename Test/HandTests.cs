namespace Pirates.Server.Domain.Test;

using System.Collections.Generic;
using Domain.Card.ImmediateResolution;
using Exception.Hand;
using NUnit.Framework;

public class HandTests
{
    private Hand _hand;

    [SetUp]
    public void SetUp()
    {
        var cards = new List<Domain.Card.Card>();

        _hand = new Hand(cards);
    }

    [Test]
    public void MustAddCard()
    {
        var rum = new Rum();

        _hand.Add(rum);

        Assert.That(_hand.Exists(rum), Is.True);
    }

    [Test]
    public void MustAddCards()
    {
        var cards = new List<Domain.Card.Card> {new Rum(), new Rum(), new Rum()};

        _hand.Add(cards);

        Assert.That(_hand.GetAll().Count, Is.EqualTo(cards.Count));
    }

    [Test]
    public void MustThrowExceptionIfCardLimitReached()
    {
        Assert.Throws<HandCardLimitReachedException>(OverfillHand);

        void OverfillHand()
        {
            _fillHand();
            _fillHand();
        }
    }

    [Test]
    public void MustGetAllCards()
    {
        _fillHand();

        Assert.That(_hand.GetAll().Count, Is.EqualTo(Hand.CardLimit));
    }

    [Test]
    public void MustGetCardById()
    {
        var rum = new Rum();

        _hand.Add(rum);

        Domain.Card.Card card = _hand.GetById(new Rum().Id);

        Assert.That(card is not null, Is.True);
    }

    [Test]
    public void MustRemoveCard()
    {
        _fillHand();

        Assert.That(_hand.GetAll().Count, Is.EqualTo(Hand.CardLimit));

        Domain.Card.Card card = _hand.GetAny();

        _hand.Remove(card);

        Assert.That(_hand.GetAll().Count, Is.EqualTo(Hand.CardLimit - 1));
    }

    [Test]
    public void MustGetAnyCard()
    {
        _fillHand();

        Domain.Card.Card card = _hand.GetAny();

        Assert.That(card is not null, Is.True);
    }

    [Test]
    public void MustGetAllCardsOfAType()
    {
        _fillHand();

        List<Rum> card = _hand.GetAll<Rum>();

        Assert.That(card.Count, Is.EqualTo(Hand.CardLimit));
    }

    [Test]
    public void MustHaveCardByType()
    {
        _hand.Add(new Rum());

        Assert.That(_hand.Exists<Rum>(), Is.True);
    }

    [Test]
    public void MustNotHaveCardByType()
    {
        Assert.That(_hand.Exists<Parrot>(), Is.False);
    }

    [Test]
    public void MustHaveCard()
    {
        _hand.Add(new Rum());

        Domain.Card.Card card = _hand.GetAny();

        Assert.That(_hand.Exists(card), Is.True);
    }

    [Test]
    public void MustNotHaveCard()
    {
        Domain.Card.Card parrot = new Parrot();

        Assert.That(_hand.Exists(parrot), Is.False);
    }

    private void _fillHand()
    {
        for (int i = 0; i < Hand.CardLimit; i++)
        {
            var rum = new Rum();

            _hand.Add(rum);
        }
    }
}

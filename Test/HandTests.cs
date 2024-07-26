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

        Assert.IsTrue(_hand.Exists(rum));
    }

    [Test]
    public void MustAddCards()
    {
        var cards = new List<Domain.Card.Card> {new Rum(), new Rum(), new Rum()};

        _hand.Add(cards);

        Assert.AreEqual(cards.Count, _hand.GetAll().Count);
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

        Assert.AreEqual(Hand.CardLimit, _hand.GetAll().Count);
    }

    [Test]
    public void MustGetCardById()
    {
        var rum = new Rum();

        _hand.Add(rum);

        Domain.Card.Card card = _hand.GetById(new Rum().Id);

        Assert.IsTrue(card is not null);
    }

    [Test]
    public void MustRemoveCard()
    {
        _fillHand();

        Assert.AreEqual(Hand.CardLimit, _hand.GetAll().Count);

        Domain.Card.Card card = _hand.GetAny();

        _hand.Remove(card);

        Assert.AreEqual(Hand.CardLimit - 1, _hand.GetAll().Count);
    }

    [Test]
    public void MustGetAnyCard()
    {
        _fillHand();

        Domain.Card.Card card = _hand.GetAny();

        Assert.IsTrue(card is not null);
    }

    [Test]
    public void MustGetAllCardsOfAType()
    {
        _fillHand();

        List<Rum> card = _hand.GetAll<Rum>();

        Assert.AreEqual(Hand.CardLimit, card.Count);
    }

    [Test]
    public void MustHaveCardByType()
    {
        _hand.Add(new Rum());

        Assert.IsTrue(_hand.Exists<Rum>());
    }

    [Test]
    public void MustNotHaveCardByType()
    {
        Assert.IsFalse(_hand.Exists<Parrot>());
    }

    [Test]
    public void MustHaveCard()
    {
        _hand.Add(new Rum());

        Domain.Card.Card card = _hand.GetAny();

        Assert.IsTrue(_hand.Exists(card));
    }

    [Test]
    public void MustNotHaveCard()
    {
        Domain.Card.Card parrot = new Parrot();

        Assert.IsFalse(_hand.Exists(parrot));
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

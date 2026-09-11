namespace Pirates.Server.Domain.Test;

using System;
using System.Collections.Generic;
using Domain.Card.Crew;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Domain.Card.Treasure;
using NUnit.Framework;

public class PlayerTests
{
    private Player _player;

    private List<Tuple<string, Domain.Card.Card>> _cardsAddAtHand;

    private List<Tuple<string, Domain.Card.Card>> _cardsRemovedAtHand;

    private List<Tuple<string, Domain.Card.Card>> _cardsAddedAtField;

    private List<Tuple<string, Domain.Card.Card>> _cardsRemovedAtField;

    [SetUp]
    public void SetUp()
    {
        string id = Guid.NewGuid().ToString();

        _cardsAddAtHand = new List<Tuple<string, Domain.Card.Card>>();
        _cardsRemovedAtHand = new List<Tuple<string, Domain.Card.Card>>();
        _cardsAddedAtField = new List<Tuple<string, Domain.Card.Card>>();
        _cardsRemovedAtField = new List<Tuple<string, Domain.Card.Card>>();

        _player = new Player(
            id,
            OnAddCardsAtHand,
            OnRemoveCardsAtHand,
            OnAddCardsAtField,
            OnRemoveCardsAtField);

        void OnAddCardsAtHand(string playerId, Domain.Card.Card card)
        {
            _cardsAddAtHand.Add(new Tuple<string, Domain.Card.Card>(playerId, card));
        }

        void OnRemoveCardsAtHand(string playerId, Domain.Card.Card card)
        {
            _cardsRemovedAtHand.Add(new Tuple<string, Domain.Card.Card>(playerId, card));
        }

        void OnAddCardsAtField(string playerId, Domain.Card.Card card)
        {
            _cardsAddedAtField.Add(new Tuple<string, Domain.Card.Card>(playerId, card));
        }

        void OnRemoveCardsAtField(string playerId, Domain.Card.Card card)
        {
            _cardsRemovedAtField.Add(new Tuple<string, Domain.Card.Card>(playerId, card));
        }
    }

    [Test]
    public void MustResetAvailableActions()
    {
        for (int i = 0; i < _player.AvailableActions; i++)
        {
            _player.SubtractAvailableActions();
        }

        const int actions = 10;

        _player.ResetAvailableActions(actions);

        Assert.That(_player.AvailableActions, Is.EqualTo(actions));
    }

    [Test]
    public void MustSubtractAvailableActions()
    {
        int expectedActions = _player.AvailableActions - 1;

        _player.SubtractAvailableActions();

        Assert.That(_player.AvailableActions, Is.EqualTo(expectedActions));
    }

    [Test]
    public void MustCalculateTreasures()
    {
        var noblePirate = new NoblePirate();

        const int handTreasures = 2;
        const int protectedTreasures = 1;
        int noblePiratesTreasures = noblePirate.Treasures;
        const int halfAmuletTreasures = 2;

        int expectedTreasures = handTreasures + protectedTreasures + noblePiratesTreasures + halfAmuletTreasures;

        _player.Hand.Add(new Treasure(handTreasures));
        _player.Hand.Add(new HalfAmulet());
        _player.Hand.Add(new HalfAmulet());

        _player.Field.AddProtected(new Treasure(protectedTreasures));

        _player.Field.Add(noblePirate);

        Assert.That(_player.CalculateTreasurePoints(), Is.EqualTo(expectedTreasures));
    }

    [Test]
    public void MustIvokeEventWhenAddingCardAtHand()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);

        Assert.That(_cardsAddAtHand[0].Item2, Is.EqualTo(rum));
        Assert.That(_cardsAddAtHand[0].Item1, Is.EqualTo(_player.Id));
    }

    [Test]
    public void MustInvokeEventWhenRemovingCardAtHand()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);
        _player.Hand.Remove(rum);

        Assert.That(_cardsRemovedAtHand[0].Item2, Is.EqualTo(rum));
        Assert.That(_cardsRemovedAtHand[0].Item1, Is.EqualTo(_player.Id));
    }

    [Test]
    public void MustInvokeEventAtAddingCardAtField()
    {
        var ironHull = new IronHull();

        _player.Field.Add(ironHull);

        Assert.That(_cardsAddedAtField[0].Item2, Is.EqualTo(ironHull));
        Assert.That(_cardsAddedAtField[0].Item1, Is.EqualTo(_player.Id));
    }

    [Test]
    public void MustInvokeEventWhenRemovingCardAtField()
    {
        var ironHull = new IronHull();

        _player.Field.Add(ironHull);

        int totalLife = ironHull.Life;

        for (int i = 0; i <= totalLife; i++)
        {
            _player.Field.DamageShip();
        }

        Assert.That(_cardsRemovedAtField[0].Item2, Is.EqualTo(ironHull));
        Assert.That(_cardsRemovedAtField[0].Item1, Is.EqualTo(_player.Id));
    }
}

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

        Assert.AreEqual(actions, _player.AvailableActions);
    }

    [Test]
    public void MustSubtractAvailableActions()
    {
        int expectedActions = _player.AvailableActions - 1;

        _player.SubtractAvailableActions();

        Assert.AreEqual(expectedActions, _player.AvailableActions);
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

        Assert.AreEqual(expectedTreasures, _player.CalculateTreasurePoints());
    }

    [Test]
    public void MustIvokeEventWhenAddingCardAtHand()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);

        Assert.AreEqual(rum, _cardsAddAtHand[0].Item2);
        Assert.AreEqual(_player.Id, _cardsAddAtHand[0].Item1);
    }

    [Test]
    public void MustInvokeEventWhenRemovingCardAtHand()
    {
        var rum = new Rum();

        _player.Hand.Add(rum);
        _player.Hand.Remove(rum);

        Assert.AreEqual(rum, _cardsRemovedAtHand[0].Item2);
        Assert.AreEqual(_player.Id, _cardsRemovedAtHand[0].Item1);
    }

    [Test]
    public void MustInvokeEventAtAddingCardAtField()
    {
        var ironHull = new IronHull();

        _player.Field.Add(ironHull);

        Assert.AreEqual(ironHull, _cardsAddedAtField[0].Item2);
        Assert.AreEqual(_player.Id, _cardsAddedAtField[0].Item1);
    }

    [Test]
    public void MustInvokeEventWhenRemovingCardAtField()
    {
        var ironHull = new IronHull();

        _player.Field.Add(ironHull);

        int vidaTotal = ironHull.Life;

        for (int i = 0; i <= vidaTotal; i++)
        {
            _player.Field.DamageShip();
        }

        Assert.AreEqual(ironHull, _cardsRemovedAtField[0].Item2);
        Assert.AreEqual(_player.Id, _cardsRemovedAtField[0].Item1);
    }
}

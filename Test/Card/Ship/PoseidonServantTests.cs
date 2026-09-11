namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Domain.Exception.Deck;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class PoseidonServantTests
{
    private Table _table;

    public PoseidonServantTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 100)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        var player1 = new Player(
            "player1",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player2 = new Player(
            "player2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player3 = new Player(
            "player3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        players.Add(player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustThrowCardNotFoundInDiscardDeckExceptionWhenDiscardDeckIsEmpty()
    {
        Player starterPlayer = _table.CurrentPlayer;

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var poseidonServant = new PoseidonServant();

        Assert.Throws<CardNotFoundInDiscardDeckException>(ApplyEffect);

        void ApplyEffect()
        {
            poseidonServant.ApplyEffect(action, _table);
        }
    }

    [Test]
    public void ApplyEffectMustReturnChooseCardInDeckOfferingEveryDiscardedCard()
    {
        Player starterPlayer = _table.CurrentPlayer;
        var discardedCard = Substitute.For<Card>();

        _table.DiscardDeck.PushTop(discardedCard);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var poseidonServant = new PoseidonServant();

        List<BaseAction> result = poseidonServant.ApplyEffect(action, _table);

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.InstanceOf<ChooseCardInDeck>());
    }

    [Test]
    public void DefaultLifeMustBeThree()
    {
        var poseidonServant = new PoseidonServant();

        Assert.That(poseidonServant.Life, Is.EqualTo(3));
    }

    [Test]
    public void TakeDamageMustReduceLife()
    {
        var poseidonServant = new PoseidonServant();

        poseidonServant.TakeDamage(1);

        Assert.That(poseidonServant.Life, Is.EqualTo(2));
    }

    [Test]
    public void TakeDamageMustThrowShipHasNoLifeExceptionWhenLifeIsAlreadyZero()
    {
        var poseidonServant = new PoseidonServant();

        poseidonServant.TakeDamage(3);

        Assert.Throws<ShipHasNoLifeException>(TakeDamage);

        void TakeDamage()
        {
            poseidonServant.TakeDamage(1);
        }
    }
}

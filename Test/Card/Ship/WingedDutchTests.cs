namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using Action;
using Deck;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Domain.Card.Treasure;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class WingedDutchTests
{
    private Table _table;

    public WingedDutchTests()
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
    public void ApplyEffectMustEndGameWhenStarterReachesFourTreasurePoints()
    {
        Player starterPlayer = _table.CurrentPlayer;

        starterPlayer.Hand.Add(new Treasure(4));

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var wingedDutch = new WingedDutch();

        wingedDutch.ApplyEffect(action, _table);

        Assert.That(_table.Winner, Is.EqualTo(starterPlayer));
    }

    [Test]
    public void ApplyEffectMustNotEndGameWhenStarterHasFewerThanFourTreasurePoints()
    {
        Player starterPlayer = _table.CurrentPlayer;

        starterPlayer.Hand.Add(new Treasure(2));

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var wingedDutch = new WingedDutch();

        wingedDutch.ApplyEffect(action, _table);

        Assert.That(_table.Winner, Is.Null);
    }

    [Test]
    public void ApplyEffectMustAlwaysReturnNull()
    {
        Player starterPlayer = _table.CurrentPlayer;

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var wingedDutch = new WingedDutch();

        List<BaseAction> result = wingedDutch.ApplyEffect(action, _table);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void DefaultLifeMustBeThree()
    {
        var wingedDutch = new WingedDutch();

        Assert.That(wingedDutch.Life, Is.EqualTo(3));
    }

    [Test]
    public void TakeDamageMustReduceLife()
    {
        var wingedDutch = new WingedDutch();

        wingedDutch.TakeDamage(1);

        Assert.That(wingedDutch.Life, Is.EqualTo(2));
    }

    [Test]
    public void TakeDamageMustThrowShipHasNoLifeExceptionWhenLifeIsAlreadyZero()
    {
        var wingedDutch = new WingedDutch();

        wingedDutch.TakeDamage(3);

        Assert.Throws<ShipHasNoLifeException>(TakeDamage);

        void TakeDamage()
        {
            wingedDutch.TakeDamage(1);
        }
    }
}

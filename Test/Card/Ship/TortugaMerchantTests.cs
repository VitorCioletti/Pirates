namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using Action;
using Action.Immediate;
using Deck;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class TortugaMerchantTests
{
    private Table _table;

    public TortugaMerchantTests()
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
    public void ApplyEffectMustReturnASingleCopyPrimmaryAction()
    {
        Player starterPlayer = _table.CurrentPlayer;

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var tortugaMerchant = new TortugaMerchant();

        List<BaseAction> result = tortugaMerchant.ApplyEffect(action, _table);

        Assert.AreEqual(1, result.Count);
        Assert.IsInstanceOf<CopyPrimmary>(result[0]);
    }

    [Test]
    public void ExecutingTheReturnedActionMustBuyACardForTheStarter()
    {
        Player starterPlayer = _table.CurrentPlayer;

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var tortugaMerchant = new TortugaMerchant();

        int cardsBeforeBuy = starterPlayer.Hand.GetCardQuantity();

        List<BaseAction> result = tortugaMerchant.ApplyEffect(action, _table);

        result[0].ApplyRule(_table);

        Assert.AreEqual(cardsBeforeBuy + 1, starterPlayer.Hand.GetCardQuantity());
    }

    [Test]
    public void DefaultLifeMustBeThree()
    {
        var tortugaMerchant = new TortugaMerchant();

        Assert.AreEqual(3, tortugaMerchant.Life);
    }

    [Test]
    public void TakeDamageMustReduceLife()
    {
        var tortugaMerchant = new TortugaMerchant();

        tortugaMerchant.TakeDamage(1);

        Assert.AreEqual(2, tortugaMerchant.Life);
    }

    [Test]
    public void TakeDamageMustThrowShipHasNoLifeExceptionWhenLifeIsAlreadyZero()
    {
        var tortugaMerchant = new TortugaMerchant();

        tortugaMerchant.TakeDamage(3);

        Assert.Throws<ShipHasNoLifeException>(TakeDamage);

        void TakeDamage()
        {
            tortugaMerchant.TakeDamage(1);
        }
    }
}

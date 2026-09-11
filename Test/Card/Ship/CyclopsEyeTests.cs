namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using NSubstitute;
using NUnit.Framework;

public class CyclopsEyeTests
{
    private Table _table;

    private Player _player1;

    private Player _player2;

    private Player _player3;

    public CyclopsEyeTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 15)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        _player1 = new Player("player1", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        _player2 = new Player("player2", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        _player3 = new Player("player3", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });

        players.Add(_player1);
        players.Add(_player2);
        players.Add(_player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustReturnChoosePlayerWithEveryOtherPlayerAsOption()
    {
        var action = Substitute.For<BaseAction>(_player1, null);
        var cyclopsEye = new CyclopsEye();

        List<BaseAction> resultantActions = cyclopsEye.ApplyEffect(action, _table);

        Assert.AreEqual(1, resultantActions.Count);

        var choosePlayer = resultantActions[0] as ChoosePlayer;

        Assert.IsNotNull(choosePlayer);
        Assert.AreEqual(_player1, choosePlayer.Starter);

        var expectedOptions = new List<string> {_player2.Id, _player3.Id};

        CollectionAssert.AreEquivalent(expectedOptions, choosePlayer.Options);
    }

    [Test]
    public void ChoosingAPlayerMustReturnLookAtPlayerCardsWithTheChosenPlayerHand()
    {
        var rumAtHand = new Rum();

        _player2.Hand.Add(rumAtHand);

        var action = Substitute.For<BaseAction>(_player1, null);
        var cyclopsEye = new CyclopsEye();

        var choosePlayer = (ChoosePlayer) cyclopsEye.ApplyEffect(action, _table)[0];

        choosePlayer.FillChoices(new List<string> {_player2.Id});

        List<BaseAction> resultantActions = choosePlayer.ApplyRule(_table);

        Assert.AreEqual(1, resultantActions.Count);

        var lookAtPlayerCards = resultantActions[0] as LookAtPlayerCards;

        Assert.IsNotNull(lookAtPlayerCards);
        Assert.AreEqual(_player1, lookAtPlayerCards.Starter);

        List<string> expectedCardIds = _player2.Hand.GetAll().Select(c => c.Id).ToList();

        CollectionAssert.AreEqual(expectedCardIds, lookAtPlayerCards.Choices);
    }
}

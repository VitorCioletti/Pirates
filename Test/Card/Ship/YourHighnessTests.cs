namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class YourHighnessTests
{
    private Table _table;

    public YourHighnessTests()
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

    // Table distributes 5 initial cards to every player, so by default all
    // three players already meet the "at least 5 cards" requirement.
    [Test]
    public void ApplyEffectMustOfferAllOtherPlayersThatHaveAtLeastFiveCards()
    {
        Player starterPlayer = _table.CurrentPlayer;
        List<Player> otherPlayers = _table.Players.Where(p => p != starterPlayer).ToList();

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var yourHighness = new YourHighness();

        List<BaseAction> result = yourHighness.ApplyEffect(action, _table);

        Assert.AreEqual(1, result.Count);

        var choosePlayer = (ChoosePlayer)result[0];

        CollectionAssert.AreEquivalent(otherPlayers.Select(p => p.Id), choosePlayer.Options);
    }

    [Test]
    public void ApplyEffectMustNotOfferPlayersWithFewerThanFiveCards()
    {
        Player starterPlayer = _table.CurrentPlayer;
        Player playerWithFewCards = _table.Players.First(p => p != starterPlayer);
        Player playerThatStillQualifies = _table.Players.First(p => p != starterPlayer && p != playerWithFewCards);

        while (playerWithFewCards.Hand.GetCardQuantity() >= 5)
            playerWithFewCards.Hand.Remove(playerWithFewCards.Hand.GetAny());

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var yourHighness = new YourHighness();

        List<BaseAction> result = yourHighness.ApplyEffect(action, _table);

        var choosePlayer = (ChoosePlayer)result[0];

        CollectionAssert.AreEquivalent(new[] {playerThatStillQualifies.Id}, choosePlayer.Options);
    }

    [Test]
    public void ChoosingAPlayerMustReturnAStealCardActionTargetingTheChosenPlayer()
    {
        Player starterPlayer = _table.CurrentPlayer;
        Player chosenPlayer = _table.Players.First(p => p != starterPlayer);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var yourHighness = new YourHighness();

        var choosePlayer = (ChoosePlayer)yourHighness.ApplyEffect(action, _table)[0];

        choosePlayer.FillChoices(new List<string> {chosenPlayer.Id});

        List<BaseAction> stealResult = choosePlayer.ApplyRule(_table);

        Assert.AreEqual(1, stealResult.Count);
        Assert.IsInstanceOf<StealCard>(stealResult[0]);
    }

    // StealCard's constructor forwards the chosen target only to compute the
    // card-choice options; it never passes it on to the base constructor's
    // "target" parameter, so BaseAction.Target stays null on every StealCard
    // instance. As a result, ApplyRule's "Target.Hand.GetById(choice)" always
    // throws instead of actually moving a card from the target to the
    // starter.
    [Test]
    public void ExecutingTheStealCardActionMustThrowBecauseTargetIsNeverAssigned()
    {
        Player starterPlayer = _table.CurrentPlayer;
        Player chosenPlayer = _table.Players.First(p => p != starterPlayer);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var yourHighness = new YourHighness();

        var choosePlayer = (ChoosePlayer)yourHighness.ApplyEffect(action, _table)[0];

        choosePlayer.FillChoices(new List<string> {chosenPlayer.Id});

        var stealCard = (StealCard)choosePlayer.ApplyRule(_table)[0];

        Card cardToSteal = chosenPlayer.Hand.GetById(stealCard.Options.First());

        stealCard.FillChoices(new List<string> {cardToSteal.Id});

        Assert.IsNull(stealCard.Target);
        Assert.Throws<NullReferenceException>(ApplyRule);

        void ApplyRule()
        {
            stealCard.ApplyRule(_table);
        }
    }

    [Test]
    public void DefaultLifeMustBeThree()
    {
        var yourHighness = new YourHighness();

        Assert.AreEqual(3, yourHighness.Life);
    }

    [Test]
    public void TakeDamageMustReduceLife()
    {
        var yourHighness = new YourHighness();

        yourHighness.TakeDamage(1);

        Assert.AreEqual(2, yourHighness.Life);
    }

    [Test]
    public void TakeDamageMustThrowShipHasNoLifeExceptionWhenLifeIsAlreadyZero()
    {
        var yourHighness = new YourHighness();

        yourHighness.TakeDamage(3);

        Assert.Throws<ShipHasNoLifeException>(TakeDamage);

        void TakeDamage()
        {
            yourHighness.TakeDamage(1);
        }
    }
}

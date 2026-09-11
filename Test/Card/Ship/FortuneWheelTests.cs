namespace Pirates.Server.Domain.Test.Card.Ship;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using NSubstitute;
using NUnit.Framework;

public class FortuneWheelTests
{
    private Table _table;

    private Player _player1;

    public FortuneWheelTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 15)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        _player1 = new Player("player1", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        var player2 = new Player("player2", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        var player3 = new Player("player3", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });

        players.Add(_player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustRemoveTopTwoCardsFromCentralDeckAndReturnLookAtDeckCards()
    {
        var cardsOnCentralDeck = new List<Card>
            {Substitute.For<Card>(), Substitute.For<Card>(), Substitute.For<Card>()};

        _table.CentralDeck.PushTop(cardsOnCentralDeck);

        int cardsAmountBeforeEffect = _table.CentralDeck.CardsAmount;

        var action = Substitute.For<BaseAction>(_player1, null);
        var fortuneWheel = new FortuneWheel();

        List<BaseAction> resultantActions = fortuneWheel.ApplyEffect(action, _table);

        Assert.That(resultantActions.Count, Is.EqualTo(1));

        var lookAtDeckCards = resultantActions[0] as LookAtDeckCards;

        Assert.That(lookAtDeckCards, Is.Not.Null);
        Assert.That(lookAtDeckCards.Starter, Is.EqualTo(_player1));
        Assert.That(_table.CentralDeck.CardsAmount, Is.EqualTo(cardsAmountBeforeEffect - 2));
    }

    [Test]
    public void ChoosingToKeepTheCardsMustReturnThemToTheTopOfTheCentralDeck()
    {
        var cardsOnCentralDeck = new List<Card> {Substitute.For<Card>(), Substitute.For<Card>()};

        _table.CentralDeck.PushTop(cardsOnCentralDeck);

        int cardsAmountBeforeEffect = _table.CentralDeck.CardsAmount;

        var action = Substitute.For<BaseAction>(_player1, null);
        var fortuneWheel = new FortuneWheel();

        var lookAtDeckCards = (LookAtDeckCards) fortuneWheel.ApplyEffect(action, _table)[0];

        lookAtDeckCards.FillChoice(true);

        List<BaseAction> result = lookAtDeckCards.ApplyRule(_table);

        Assert.That(result, Is.Null);
        Assert.That(_table.CentralDeck.CardsAmount, Is.EqualTo(cardsAmountBeforeEffect));
    }

    [Test]
    public void ChoosingToDiscardTheCardsMustSendThemToTheBottomOfTheCentralDeck()
    {
        var firstCard = Substitute.For<Card>();
        var secondCard = Substitute.For<Card>();

        _table.CentralDeck.PushTop(new List<Card> {firstCard, secondCard});

        var action = Substitute.For<BaseAction>(_player1, null);
        var fortuneWheel = new FortuneWheel();

        var lookAtDeckCards = (LookAtDeckCards) fortuneWheel.ApplyEffect(action, _table)[0];

        lookAtDeckCards.FillChoice(false);

        lookAtDeckCards.ApplyRule(_table);

        int totalCardsRemaining = _table.CentralDeck.CardsAmount;

        List<Card> allCardsFromTopToBottom = _table.CentralDeck.GetTop(totalCardsRemaining);

        List<Card> lastTwoCards =
            allCardsFromTopToBottom.GetRange(allCardsFromTopToBottom.Count - 2, 2);

        Assert.That(lastTwoCards, Does.Contain(firstCard));
        Assert.That(lastTwoCards, Does.Contain(secondCard));
    }
}

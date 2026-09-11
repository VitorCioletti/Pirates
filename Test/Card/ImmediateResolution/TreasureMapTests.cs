namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using NSubstitute;
using NUnit.Framework;

public class TreasureMapTests
{
    private Table _table;

    private Player _player1;

    public TreasureMapTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 30)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        _player1 = new Player(
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

        players.Add(_player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustReturnChooseCardInDeckWithFourCardsFromCentralDeck()
    {
        int cardsInDeckBeforeEffect = _table.CentralDeck.CardsAmount;

        var action = Substitute.For<BaseAction>(_player1, null);

        var treasureMap = new TreasureMap();

        List<BaseAction> result = treasureMap.ApplyEffect(action, _table);

        Assert.That(result.Count, Is.EqualTo(1));

        var chooseCardInDeck = result[0] as ChooseCardInDeck;

        Assert.That(chooseCardInDeck, Is.Not.Null);
        Assert.That(chooseCardInDeck.Options.Count, Is.EqualTo(4));
        Assert.That(_table.CentralDeck.CardsAmount, Is.EqualTo(cardsInDeckBeforeEffect - 4));
    }

    [Test]
    public void ChooseCardInDeckApplyRuleMustAddChosenCardToStarterHandAndReturnRestToDeck()
    {
        int cardsInDeckBeforeEffect = _table.CentralDeck.CardsAmount;
        int handCardsBeforeEffect = _player1.Hand.GetCardQuantity();

        var action = Substitute.For<BaseAction>(_player1, null);

        var treasureMap = new TreasureMap();

        List<BaseAction> result = treasureMap.ApplyEffect(action, _table);

        var chooseCardInDeck = (ChooseCardInDeck) result[0];

        string chosenId = chooseCardInDeck.Options[0];

        chooseCardInDeck.FillChoices(new List<string> {chosenId});
        chooseCardInDeck.ApplyRule(_table);

        Assert.That(_player1.Hand.GetCardQuantity(), Is.EqualTo(handCardsBeforeEffect + 1));
        Assert.That(_player1.Hand.GetById(chosenId), Is.Not.Null);
        Assert.That(_table.CentralDeck.CardsAmount, Is.EqualTo(cardsInDeckBeforeEffect - 1));
    }
}

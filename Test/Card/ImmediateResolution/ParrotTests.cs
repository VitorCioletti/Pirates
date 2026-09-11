namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Action.Immediate;
using Action.Primary;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class ParrotTests
{
    private Table _table;

    private Player _player1;
    private Player _player2;
    private Player _player3;

    public ParrotTests()
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

        _player2 = new Player(
            "player2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        _player3 = new Player(
            "player3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        players.Add(_player1);
        players.Add(_player2);
        players.Add(_player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustThrowHasNoValidActionExceptionWhenThereIsNoMatchingLastAction()
    {
        var action = Substitute.For<BaseAction>(_player1, null);

        var parrot = new Parrot();

        Assert.Throws<HasNoValidActionException>(() => parrot.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowImpossibleToCopyExceptionWhenDrawnCardCannotBeCopied()
    {
        var notAllowedCard = Substitute.For<Card>();
        var drawCard = new DrawCard(_player1, notAllowedCard) {Turn = 0};

        _table.ActionHistory.Push(drawCard);

        var action = Substitute.For<BaseAction>(_player1, null);

        var parrot = new Parrot();

        Assert.Throws<ImpossibleToCopyException>(() => parrot.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustProcessCopiedDrawCardWhenCardIsAllowedToBeCopied()
    {
        int handCardsBeforeEffect = _player1.Hand.GetCardQuantity();

        var rumToCopy = new Rum();

        _player1.Hand.Add(rumToCopy);

        var drawCard = new DrawCard(_player1, rumToCopy) {Turn = 0};

        _table.ActionHistory.Push(drawCard);

        var action = Substitute.For<BaseAction>(_player1, null);

        var parrot = new Parrot();

        List<BaseAction> result = parrot.ApplyEffect(action, _table);

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
        Assert.IsFalse(_player1.Hand.Exists(rumToCopy));
        Assert.AreEqual(handCardsBeforeEffect + 2, _player1.Hand.GetCardQuantity());
        Assert.AreEqual(1, _table.DiscardDeck.CardsAmount);
    }

    [Test]
    public void ApplyEffectMustReturnChoosePlayerWhenLastActionIsADuel()
    {
        var starterCard = new Domain.Card.Duel.Cannon();
        var duel = new Duel(_player1, null, starterCard) {Turn = 0};

        _table.ActionHistory.Push(duel);

        var action = Substitute.For<BaseAction>(_player1, null);

        var parrot = new Parrot();

        List<BaseAction> result = parrot.ApplyEffect(action, _table);

        Assert.AreEqual(1, result.Count);

        var choosePlayer = result[0] as ChoosePlayer;

        Assert.IsNotNull(choosePlayer);
        Assert.AreEqual(2, choosePlayer.Options.Count);
        CollectionAssert.DoesNotContain(choosePlayer.Options, _player1.Id);
        CollectionAssert.Contains(choosePlayer.Options, _player2.Id);
        CollectionAssert.Contains(choosePlayer.Options, _player3.Id);

        choosePlayer.FillChoices(new List<string> {_player2.Id});

        List<BaseAction> afterChoiceResult = choosePlayer.ApplyRule(_table);

        Assert.AreEqual(1, afterChoiceResult.Count);
        Assert.IsInstanceOf<CopyPrimmary>(afterChoiceResult[0]);
    }
}

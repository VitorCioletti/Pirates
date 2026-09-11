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

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(0));
        Assert.That(_player1.Hand.Exists(rumToCopy), Is.False);
        Assert.That(_player1.Hand.GetCardQuantity(), Is.EqualTo(handCardsBeforeEffect + 2));
        Assert.That(_table.DiscardDeck.CardsAmount, Is.EqualTo(1));
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

        Assert.That(result.Count, Is.EqualTo(1));

        var choosePlayer = result[0] as ChoosePlayer;

        Assert.That(choosePlayer, Is.Not.Null);
        Assert.That(choosePlayer.Options.Count, Is.EqualTo(2));
        Assert.That(choosePlayer.Options, Does.Not.Contain(_player1.Id));
        Assert.That(choosePlayer.Options, Does.Contain(_player2.Id));
        Assert.That(choosePlayer.Options, Does.Contain(_player3.Id));

        choosePlayer.FillChoices(new List<string> {_player2.Id});

        List<BaseAction> afterChoiceResult = choosePlayer.ApplyRule(_table);

        Assert.That(afterChoiceResult.Count, Is.EqualTo(1));
        Assert.That(afterChoiceResult[0], Is.InstanceOf<CopyPrimmary>());
    }
}

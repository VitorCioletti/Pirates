namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Domain.Exception.Deck;
using NSubstitute;
using NUnit.Framework;

public class DiverTests
{
    private Table _table;

    public DiverTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Diver), 1)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        var player1 = new Player("player1", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        var player2 = new Player("player2", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
        var player3 = new Player("player3", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });

        players.Add(player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void ApplyEffectMustThrowCardNotFoundInDiscardDeckExceptionWhenDiscardDeckIsEmpty()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var diver = new Diver();

        Assert.Throws<CardNotFoundInDiscardDeckException>(() => diver.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustReturnChooseCardInDeckOfferingEveryDiscardedCard()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var firstDiscardedCard = Substitute.For<Card>();
        var secondDiscardedCard = Substitute.For<Card>();

        _table.DiscardDeck.PushTop(new List<Card> {firstDiscardedCard, secondDiscardedCard});

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var diver = new Diver();

        List<BaseAction> result = diver.ApplyEffect(action, _table);

        Assert.That(result.Count, Is.EqualTo(1));

        var chooseCardInDeck = result[0] as ChooseCardInDeck;

        Assert.That(chooseCardInDeck, Is.Not.Null);
        Assert.That(chooseCardInDeck.Options.Count, Is.EqualTo(2));
    }
}

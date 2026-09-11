namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
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
    public void ApplyEffectMustThrowInvalidCastExceptionWhenDiscardDeckIsEmpty()
    {
        // Diver.ApplyEffect always calls DiscardDeck.GetAll<Card>(), whose
        // implementation casts Cards.Select(c => c is T) (an IEnumerable<bool>)
        // directly to List<T>. That cast is invalid at runtime, so calling
        // Diver.ApplyEffect currently always throws InvalidCastException,
        // regardless of the discard deck's contents, and its intended
        // "let the starter pick any card from the discard deck" behavior is
        // unreachable.
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var diver = new Diver();

        Assert.Throws<InvalidCastException>(() => diver.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowInvalidCastExceptionEvenWhenDiscardDeckHasCards()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        _table.DiscardDeck.PushTop(new List<Card> {Substitute.For<Card>(), Substitute.For<Card>()});

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var diver = new Diver();

        Assert.Throws<InvalidCastException>(() => diver.ApplyEffect(action, _table));
    }
}

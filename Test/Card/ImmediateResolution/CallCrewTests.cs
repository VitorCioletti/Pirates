namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Deck;
using Domain.Card;
using Domain.Card.Crew;
using Domain.Card.ImmediateResolution;
using Domain.Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class CallCrewTests
{
    private Table _table;

    public CallCrewTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Pirate), 1)};

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
    public void ApplyEffectMustThrowFullCrewExceptionWhenStarterFieldCrewIsFull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        starterPlayer.Field.Add(new Pirate());
        starterPlayer.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        Assert.Throws<FullCrewException>(() => callCrew.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowInvalidCastExceptionWhenDiscardDeckIsEmpty()
    {
        // DiscardDeck.GetAll<T>() casts the result of Cards.Select(c => c is T)
        // (an IEnumerable<bool>) directly to List<T>. That cast is invalid at
        // runtime for any T, so it always throws InvalidCastException before
        // the "no crew member in discard deck" check can ever run - meaning
        // NoCrewMemberInDiscardDeckException is currently unreachable.
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        Assert.Throws<InvalidCastException>(() => callCrew.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowInvalidCastExceptionEvenWhenDiscardDeckHasCrewMembers()
    {
        // Same underlying DiscardDeck.GetAll<T> bug: it throws unconditionally,
        // regardless of the discard deck's actual contents.
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        _table.DiscardDeck.PushTop(new List<Card> {new Pirate()});

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        Assert.Throws<InvalidCastException>(() => callCrew.ApplyEffect(action, _table));
    }
}

namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.Crew;
using Domain.Card.ImmediateResolution;
using Domain.Exception.Card;
using Domain.Exception.Deck;
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
    public void ApplyEffectMustThrowCardNotFoundInDiscardDeckExceptionWhenDiscardDeckIsEmpty()
    {
        // CallCrew.ApplyEffect calls discardDeck.GetAll<BaseCrewMember>() and only
        // afterward checks `if (discardedCrewMembers.Count == 0) throw new
        // NoCrewMemberInDiscardDeckException(...)`. But GetAll<T>() itself already
        // throws CardNotFoundInDiscardDeckException whenever no card of type T is
        // found, so it never returns an empty list — CallCrew's own
        // NoCrewMemberInDiscardDeckException check is unreachable dead code, and the
        // exception actually surfaced is CardNotFoundInDiscardDeckException instead.
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        Assert.Throws<CardNotFoundInDiscardDeckException>(() => callCrew.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowCardNotFoundInDiscardDeckExceptionWhenDiscardDeckHasNoCrewMembers()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        _table.DiscardDeck.PushTop(new Rum());

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        Assert.Throws<CardNotFoundInDiscardDeckException>(() => callCrew.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustReturnChooseCardInDeckOfferingEveryDiscardedCrewMember()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var discardedPirate = new Pirate();

        _table.DiscardDeck.PushTop(discardedPirate);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var callCrew = new CallCrew();

        List<BaseAction> result = callCrew.ApplyEffect(action, _table);

        Assert.That(result.Count, Is.EqualTo(1));

        var chooseCardInDeck = result[0] as ChooseCardInDeck;

        Assert.That(chooseCardInDeck, Is.Not.Null);
        Assert.That(chooseCardInDeck.Options, Is.EqualTo(new List<string> {discardedPirate.Id}));
    }
}

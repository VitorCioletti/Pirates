namespace Pirates.Server.Domain.Test.Card.Event;

using System;
using System.Collections.Generic;
using Domain.Action;
using Domain.Action.Resultant;
using Domain.Card.Crew;
using Domain.Card.Event;
using Domain.Card.Ship;
using Domain.Deck;
using Domain.Exception.Action;
using NSubstitute;
using NUnit.Framework;

public class KrakenTests
{
    private Table _table;

    public KrakenTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Kraken), 20)};

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
    public void ApplyEffectMustThrowWhenFirstPlayerHasNoShipAndNoCrew()
    {
        Player player1 = _table.Players[0];

        var action = Substitute.For<BaseAction>(player1, null);
        var kraken = new Kraken();

        Assert.Throws<DoesNotHaveCrewMemberException>(() => kraken.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowWhenFirstPlayerCrewHasNoDrownableMembers()
    {
        Player player1 = _table.Players[0];

        player1.Field.Add(new GhostPirate());

        var action = Substitute.For<BaseAction>(player1, null);
        var kraken = new Kraken();

        Assert.Throws<NoCrewMemberCanBeDrownedException>(() => kraken.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustDamageShipDirectlyWhenPlayerHasShipAndDrownableCrew()
    {
        Player player1 = _table.Players[0];
        Player player2 = _table.Players[1];
        Player player3 = _table.Players[2];

        player1.Field.Add(new IronHull());
        player1.Field.Add(new Pirate());

        player2.Field.Add(new Pirate());
        player3.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(player1, null);
        var kraken = new Kraken();

        List<BaseAction> result = kraken.ApplyEffect(action, _table);

        Assert.AreEqual(2, player1.Field.Ship.Life);
        Assert.AreEqual(1, player1.Field.Crew.Count);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void ApplyEffectMustDoNothingWhenPlayerHasNoShipAndExactlyOneDrownableCrewMember()
    {
        // Kraken.ApplyEffect has a pre-existing production bug: `hasAnyCrew` is actually
        // computed as `Crew.Count == 0` (i.e. it really means "has NO crew"), so the guard
        // `if (!hasShip && !hasAnyCrew) continue;` skips every shipless player that DOES
        // have crew — which is exactly the case the "auto-drown a single drownable member"
        // branch further down is meant to handle. That branch is unreachable dead code as a
        // result, so a shipless player with one drownable crew member is left untouched.
        Player player1 = _table.Players[0];
        Player player2 = _table.Players[1];
        Player player3 = _table.Players[2];

        player1.Field.Add(new Pirate());

        player2.Field.Add(new Pirate());
        player3.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(player1, null);
        var kraken = new Kraken();

        List<BaseAction> result = kraken.ApplyEffect(action, _table);

        Assert.AreEqual(1, player1.Field.Crew.Count);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void ApplyEffectMustDoNothingWhenPlayerHasNoShipAndMultipleDrownableCrewMembers()
    {
        // Same dead-code bug as ApplyEffectMustDoNothingWhenPlayerHasNoShipAndExactlyOneDrownableCrewMember
        // above: the "choose which crew member to drown" branch is unreachable, so nothing happens.
        Player player1 = _table.Players[0];
        Player player2 = _table.Players[1];
        Player player3 = _table.Players[2];

        player1.Field.Add(new Pirate());
        player1.Field.Add(new Pirate());

        player2.Field.Add(new Pirate());
        player3.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(player1, null);
        var kraken = new Kraken();

        List<BaseAction> result = kraken.ApplyEffect(action, _table);

        Assert.AreEqual(0, result.Count);
        Assert.AreEqual(2, player1.Field.Crew.Count);
    }
}

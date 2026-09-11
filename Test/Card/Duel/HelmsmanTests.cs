namespace Pirates.Server.Domain.Test.Card.Duel;

using System;
using System.Collections.Generic;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card.Duel;
using Domain.Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class HelmsmanTests
{
    private Table _table;

    public HelmsmanTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Helmsman), 1)};

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
    public void ApplyEffectMustEndDuelModeWhenActionIsDrawDuelAnswerCardAndReturnNull()
    {
        var starterPlayer = new Player("starter", null, null, null, null);
        var targetPlayer = new Player("target", null, null, null, null);
        var action = Substitute.For<DrawDuelAnswerCard>(null, starterPlayer, targetPlayer);
        var helmsman = new Helmsman();

        _table.EnterDuelMode();

        List<BaseAction> result = helmsman.ApplyEffect(action, _table);

        Assert.That(_table.InDuel, Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ApplyEffectMustThrowWhenActionIsNotDrawDuelAnswerCard()
    {
        var starterPlayer = new Player("starter", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var helmsman = new Helmsman();

        Assert.Throws<CanOnlyBeUsedInDuelException>(() => helmsman.ApplyEffect(action, _table));
    }

    [Test]
    public void ApplyEffectMustThrowWhenTableIsNotInDuelMode()
    {
        var starterPlayer = new Player("starter", null, null, null, null);
        var targetPlayer = new Player("target", null, null, null, null);
        var action = Substitute.For<DrawDuelAnswerCard>(null, starterPlayer, targetPlayer);
        var helmsman = new Helmsman();

        Assert.Throws<Domain.Exception.Table.NoDuelException>(() => helmsman.ApplyEffect(action, _table));
    }
}

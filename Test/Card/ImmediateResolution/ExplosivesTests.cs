namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Action.Resultant;
using Deck;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using NSubstitute;
using NUnit.Framework;

public class ExplosivesTests
{
    private Table _table;

    public ExplosivesTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Explosives), 1)};

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
    public void ApplyEffectMustReturnDistributeCardsWithTopThreeCentralDeckCards()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var cardsOnTop = new List<Card> {Substitute.For<Card>(), Substitute.For<Card>(), Substitute.For<Card>()};

        _table.CentralDeck.PushTop(cardsOnTop);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var explosives = new Explosives();

        List<BaseAction> result = explosives.ApplyEffect(action, _table);

        Assert.AreEqual(1, result.Count);

        var distributeCards = result[0] as DistributeCards;

        Assert.IsNotNull(distributeCards);
        Assert.AreEqual(starterPlayer, distributeCards.Starter);
        Assert.AreEqual(2, distributeCards.LimitValuePerKey);

        foreach (Card card in cardsOnTop)
            Assert.IsTrue(distributeCards.ValueOptions.Contains(card.Id));

        CollectionAssert.AreEquivalent(
            _table.Players.Select(p => p.Id),
            distributeCards.KeysOptions);

        Assert.IsNull(_table.CentralDeck.GetTop());
    }

    [Test]
    public void ApplyEffectMustThrowNullReferenceExceptionWhenCentralDeckHasFewerThanThreeCards()
    {
        // After the table is built, the central deck configured with a single
        // card type is already fully consumed by the initial hand distribution.
        // Explosives always asks for the top 3 cards regardless of how many
        // remain, so CentralDeck.GetTop(3) pads the result with null entries,
        // and building the resultant DistributeCards action (which reads
        // card.Id for every card) throws NullReferenceException instead of
        // gracefully distributing fewer cards.
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var explosives = new Explosives();

        Assert.Throws<NullReferenceException>(() => explosives.ApplyEffect(action, _table));
    }
}

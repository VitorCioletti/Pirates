namespace Pirates.Server.Domain.Test.Card.Treasure;

using System.Collections.Generic;
using Domain.Action;
using Domain.Card.Treasure;
using NSubstitute;
using NUnit.Framework;

public class HalfAmuletTests
{
    [Test]
    public void ConstructorMustSetValueToZero()
    {
        var halfAmulet = new HalfAmulet();

        Assert.AreEqual(0, halfAmulet.Value);
    }

    [Test]
    public void ApplyEffectMustReturnNull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var halfAmulet = new HalfAmulet();

        var result = halfAmulet.ApplyEffect(action, null);

        Assert.IsNull(result);
    }

    [Test]
    public void CalulateTreasurePointsMustReturnZeroWhenNoAmulets()
    {
        var amulets = new List<HalfAmulet>();

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.AreEqual(0, points);
    }

    [Test]
    public void CalulateTreasurePointsMustReturnZeroForSingleAloneAmulet()
    {
        var amulets = new List<HalfAmulet> {new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.AreEqual(0, points);
    }

    [Test]
    public void CalulateTreasurePointsMustReturnTwoForAPairOfAmulets()
    {
        var amulets = new List<HalfAmulet> {new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.AreEqual(2, points);
    }

    [Test]
    public void CalulateTreasurePointsMustIgnoreLeftoverAloneAmuletAfterPairs()
    {
        var amulets = new List<HalfAmulet> {new(), new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.AreEqual(2, points);
    }

    [Test]
    public void CalulateTreasurePointsMustReturnFourForTwoPairsOfAmulets()
    {
        var amulets = new List<HalfAmulet> {new(), new(), new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.AreEqual(4, points);
    }

    [Test]
    public void PlayerCalculateTreasurePointsMustCountHalfAmuletPairInHand()
    {
        var player = new Player(string.Empty, null, null, null, null);

        player.Hand.Add(new HalfAmulet());
        player.Hand.Add(new HalfAmulet());

        Assert.AreEqual(2, player.CalculateTreasurePoints());
    }

    [Test]
    public void PlayerCalculateTreasurePointsMustNotCountLoneHalfAmuletInHand()
    {
        var player = new Player(string.Empty, null, null, null, null);

        player.Hand.Add(new HalfAmulet());

        Assert.AreEqual(0, player.CalculateTreasurePoints());
    }
}

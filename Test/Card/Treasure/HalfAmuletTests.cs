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

        Assert.That(halfAmulet.Value, Is.EqualTo(0));
    }

    [Test]
    public void ApplyEffectMustReturnNull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var halfAmulet = new HalfAmulet();

        var result = halfAmulet.ApplyEffect(action, null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void CalulateTreasurePointsMustReturnZeroWhenNoAmulets()
    {
        var amulets = new List<HalfAmulet>();

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.That(points, Is.EqualTo(0));
    }

    [Test]
    public void CalulateTreasurePointsMustReturnZeroForSingleAloneAmulet()
    {
        var amulets = new List<HalfAmulet> {new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.That(points, Is.EqualTo(0));
    }

    [Test]
    public void CalulateTreasurePointsMustReturnTwoForAPairOfAmulets()
    {
        var amulets = new List<HalfAmulet> {new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.That(points, Is.EqualTo(2));
    }

    [Test]
    public void CalulateTreasurePointsMustIgnoreLeftoverAloneAmuletAfterPairs()
    {
        var amulets = new List<HalfAmulet> {new(), new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.That(points, Is.EqualTo(2));
    }

    [Test]
    public void CalulateTreasurePointsMustReturnFourForTwoPairsOfAmulets()
    {
        var amulets = new List<HalfAmulet> {new(), new(), new(), new()};

        int points = HalfAmulet.CalulateTreasurePoints(amulets);

        Assert.That(points, Is.EqualTo(4));
    }

    [Test]
    public void PlayerCalculateTreasurePointsMustCountHalfAmuletPairInHand()
    {
        var player = new Player(string.Empty, null, null, null, null);

        player.Hand.Add(new HalfAmulet());
        player.Hand.Add(new HalfAmulet());

        Assert.That(player.CalculateTreasurePoints(), Is.EqualTo(2));
    }

    [Test]
    public void PlayerCalculateTreasurePointsMustNotCountLoneHalfAmuletInHand()
    {
        var player = new Player(string.Empty, null, null, null, null);

        player.Hand.Add(new HalfAmulet());

        Assert.That(player.CalculateTreasurePoints(), Is.EqualTo(0));
    }
}

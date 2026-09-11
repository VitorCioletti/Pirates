namespace Pirates.Server.Domain.Test.Card.Duel;

using System.Collections.Generic;
using Action;
using Domain.Card.Duel;
using NSubstitute;
using NUnit.Framework;

public class CannonTests
{
    [Test]
    public void ConstructorMustSetShotsToOne()
    {
        var cannon = new Cannon();

        Assert.AreEqual(1, cannon.Shots);
    }

    [Test]
    public void ApplyEffectMustAddCannonToStarterFieldAndReturnNull()
    {
        var starterPlayer = new Player("player1", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var cannon = new Cannon();

        List<BaseAction> result = cannon.ApplyEffect(action, null);

        Assert.IsTrue(starterPlayer.Field.Cannons.Contains(cannon));
        Assert.IsNull(result);
    }

    [Test]
    public void ApplyEffectMustIncreaseFieldDuelShotsByCannonShots()
    {
        var starterPlayer = new Player("player1", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var cannon = new Cannon();

        cannon.ApplyEffect(action, null);

        Assert.AreEqual(cannon.Shots, starterPlayer.Field.CalculateDuelShots());
    }

    [Test]
    public void ApplyEffectMustAddEachCannonWhenUsedMultipleTimes()
    {
        var starterPlayer = new Player("player1", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var firstCannon = new Cannon();
        var secondCannon = new Cannon();

        firstCannon.ApplyEffect(action, null);
        secondCannon.ApplyEffect(action, null);

        Assert.AreEqual(2, starterPlayer.Field.Cannons.Count);

        Assert.AreEqual(firstCannon.Shots + secondCannon.Shots, starterPlayer.Field.CalculateDuelShots());
    }
}

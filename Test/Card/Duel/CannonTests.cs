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

        Assert.That(cannon.Shots, Is.EqualTo(1));
    }

    [Test]
    public void ApplyEffectMustAddCannonToStarterFieldAndReturnNull()
    {
        var starterPlayer = new Player("player1", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var cannon = new Cannon();

        List<BaseAction> result = cannon.ApplyEffect(action, null);

        Assert.That(starterPlayer.Field.Cannons.Contains(cannon), Is.True);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ApplyEffectMustIncreaseFieldDuelShotsByCannonShots()
    {
        var starterPlayer = new Player("player1", null, null, null, null);
        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var cannon = new Cannon();

        cannon.ApplyEffect(action, null);

        Assert.That(starterPlayer.Field.CalculateDuelShots(), Is.EqualTo(cannon.Shots));
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

        Assert.That(starterPlayer.Field.Cannons.Count, Is.EqualTo(2));

        Assert.That(
            starterPlayer.Field.CalculateDuelShots(),
            Is.EqualTo(firstCannon.Shots + secondCannon.Shots));
    }
}

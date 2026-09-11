namespace Pirates.Server.Domain.Test.Card.SurpriseDuel;

using System.Collections.Generic;
using Domain.Action;
using Domain.Card.SurpriseDuel;
using NSubstitute;
using NUnit.Framework;

public class SurpriseAttackTests
{
    [Test]
    public void ConstructorMustSetShotsToOne()
    {
        var surpriseAttack = new SurpriseAttack();

        Assert.AreEqual(1, surpriseAttack.Shots);
    }

    [Test]
    public void ApplyEffectMustAddCardToStarterFieldSurpriseDuelListAndReturnNull()
    {
        var starterPlayer = new Player("player1", null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var surpriseAttack = new SurpriseAttack();

        List<BaseAction> result = surpriseAttack.ApplyEffect(action, null);

        Assert.IsTrue(starterPlayer.Field.SurpriseDuel.Contains(surpriseAttack));
        Assert.IsNull(result);
    }

    [Test]
    public void ApplyEffectMustIncreaseFieldDuelShotsBySurpriseAttackShots()
    {
        var starterPlayer = new Player("player1", null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var surpriseAttack = new SurpriseAttack();

        surpriseAttack.ApplyEffect(action, null);

        Assert.AreEqual(surpriseAttack.Shots, starterPlayer.Field.CalculateDuelShots());
    }

    [Test]
    public void ApplyEffectMustAddEachSurpriseAttackWhenUsedMultipleTimes()
    {
        var starterPlayer = new Player("player1", null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var firstSurpriseAttack = new SurpriseAttack();
        var secondSurpriseAttack = new SurpriseAttack();

        firstSurpriseAttack.ApplyEffect(action, null);
        secondSurpriseAttack.ApplyEffect(action, null);

        Assert.AreEqual(2, starterPlayer.Field.SurpriseDuel.Count);

        Assert.AreEqual(
            firstSurpriseAttack.Shots + secondSurpriseAttack.Shots,
            starterPlayer.Field.CalculateDuelShots());
    }
}

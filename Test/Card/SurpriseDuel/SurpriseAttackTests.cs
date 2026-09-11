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

        Assert.That(surpriseAttack.Shots, Is.EqualTo(1));
    }

    [Test]
    public void ApplyEffectMustAddCardToStarterFieldSurpriseDuelListAndReturnNull()
    {
        var starterPlayer = new Player("player1", null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var surpriseAttack = new SurpriseAttack();

        List<BaseAction> result = surpriseAttack.ApplyEffect(action, null);

        Assert.That(starterPlayer.Field.SurpriseDuel.Contains(surpriseAttack), Is.True);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ApplyEffectMustIncreaseFieldDuelShotsBySurpriseAttackShots()
    {
        var starterPlayer = new Player("player1", null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var surpriseAttack = new SurpriseAttack();

        surpriseAttack.ApplyEffect(action, null);

        Assert.That(starterPlayer.Field.CalculateDuelShots(), Is.EqualTo(surpriseAttack.Shots));
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

        Assert.That(starterPlayer.Field.SurpriseDuel.Count, Is.EqualTo(2));

        Assert.That(
            starterPlayer.Field.CalculateDuelShots(),
            Is.EqualTo(firstSurpriseAttack.Shots + secondSurpriseAttack.Shots));
    }
}

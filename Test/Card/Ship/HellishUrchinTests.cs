namespace Pirates.Server.Domain.Test.Card.Ship;

using Domain.Card.Duel;
using Domain.Card.Ship;
using Exception.Card;
using NUnit.Framework;

public class HellishUrchinTests
{
    [Test]
    public void ApplyEffectMustReturnNull()
    {
        var hellishUrchin = new HellishUrchin();

        var result = hellishUrchin.ApplyEffect(null, null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MustHaveDefaultShotsAndLife()
    {
        var hellishUrchin = new HellishUrchin();

        Assert.That(hellishUrchin.Shots, Is.EqualTo(3));
        Assert.That(hellishUrchin.Life, Is.EqualTo(3));
    }

    [Test]
    public void TakeDamageMustReduceLifeAndThrowWhenAlreadyDead()
    {
        var hellishUrchin = new HellishUrchin();

        hellishUrchin.TakeDamage(1);
        Assert.That(hellishUrchin.Life, Is.EqualTo(2));

        hellishUrchin.TakeDamage(2);
        Assert.That(hellishUrchin.Life, Is.EqualTo(0));

        Assert.Throws<ShipHasNoLifeException>(() => hellishUrchin.TakeDamage(1));
    }

    [Test]
    public void FieldMustOnlyAddHellishUrchinShotsWhenThereAreOtherDuelShots()
    {
        var field = new Field();
        var hellishUrchin = new HellishUrchin();

        field.Add(hellishUrchin);

        Assert.That(field.CalculateDuelShots(), Is.EqualTo(0));

        var cannon = new Cannon();

        field.Add(cannon);

        Assert.That(field.CalculateDuelShots(), Is.EqualTo(cannon.Shots + hellishUrchin.Shots));
    }
}

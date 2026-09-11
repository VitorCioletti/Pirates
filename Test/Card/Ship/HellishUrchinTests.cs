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

        Assert.IsNull(result);
    }

    [Test]
    public void MustHaveDefaultShotsAndLife()
    {
        var hellishUrchin = new HellishUrchin();

        Assert.AreEqual(3, hellishUrchin.Shots);
        Assert.AreEqual(3, hellishUrchin.Life);
    }

    [Test]
    public void TakeDamageMustReduceLifeAndThrowWhenAlreadyDead()
    {
        var hellishUrchin = new HellishUrchin();

        hellishUrchin.TakeDamage(1);
        Assert.AreEqual(2, hellishUrchin.Life);

        hellishUrchin.TakeDamage(2);
        Assert.AreEqual(0, hellishUrchin.Life);

        Assert.Throws<ShipHasNoLifeException>(() => hellishUrchin.TakeDamage(1));
    }

    [Test]
    public void FieldMustOnlyAddHellishUrchinShotsWhenThereAreOtherDuelShots()
    {
        var field = new Field();
        var hellishUrchin = new HellishUrchin();

        field.Add(hellishUrchin);

        Assert.AreEqual(0, field.CalculateDuelShots());

        var cannon = new Cannon();

        field.Add(cannon);

        Assert.AreEqual(cannon.Shots + hellishUrchin.Shots, field.CalculateDuelShots());
    }
}

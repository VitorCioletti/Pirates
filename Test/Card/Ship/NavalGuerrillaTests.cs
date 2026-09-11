namespace Pirates.Server.Domain.Test.Card.Ship;

using System.Collections.Generic;
using System.Linq;
using Action;
using Domain.Card.Duel;
using Domain.Card.Ship;
using Exception.Card;
using NUnit.Framework;

public class NavalGuerrillaTests
{
    [Test]
    public void ApplyEffectMustReturnNull()
    {
        var navalGuerrilla = new NavalGuerrilla();

        List<BaseAction> result = navalGuerrilla.ApplyEffect(null, null);

        Assert.IsNull(result);
    }

    [Test]
    public void DefaultAdditionalShotsMustBeTwo()
    {
        var navalGuerrilla = new NavalGuerrilla();

        Assert.AreEqual(2, navalGuerrilla.AdditionalShots);
    }

    [Test]
    public void DefaultLifeMustBeThree()
    {
        var navalGuerrilla = new NavalGuerrilla();

        Assert.AreEqual(3, navalGuerrilla.Life);
    }

    [Test]
    public void TakeDamageMustReduceLife()
    {
        var navalGuerrilla = new NavalGuerrilla();

        navalGuerrilla.TakeDamage(1);

        Assert.AreEqual(2, navalGuerrilla.Life);
    }

    [Test]
    public void TakeDamageMustThrowShipHasNoLifeExceptionWhenLifeIsAlreadyZero()
    {
        var navalGuerrilla = new NavalGuerrilla();

        navalGuerrilla.TakeDamage(3);

        Assert.Throws<ShipHasNoLifeException>(TakeDamage);

        void TakeDamage()
        {
            navalGuerrilla.TakeDamage(1);
        }
    }

    [Test]
    public void FieldMustMultiplyDuelShotsByAdditionalShotsWhenShipIsNavalGuerrilla()
    {
        var field = new Field();
        var navalGuerrilla = new NavalGuerrilla();
        var cannons = new List<Cannon> {new(), new()};

        field.Add(navalGuerrilla);
        field.Add(cannons);

        int cannonShots = cannons.Sum(c => c.Shots);
        int expectedShots = cannonShots + navalGuerrilla.AdditionalShots * cannons.Count;

        Assert.AreEqual(expectedShots, field.CalculateDuelShots());
    }
}

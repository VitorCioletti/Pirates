namespace Pirates.Server.Domain.Test;

using System.Collections.Generic;
using Domain.Card.Crew;
using Domain.Card.Duel;
using Domain.Card.Ship;
using Domain.Card.SurpriseDuel;
using Domain.Card.Treasure;
using Exception.Field;
using NUnit.Framework;

public class FieldTests
{
    private Field _field;

    [SetUp]
    public void SetUp()
    {
        _field = new Field();
    }

    [Test]
    public void MustDamageShip()
    {
        var ironhull = new IronHull();

        int lifePreDamage = ironhull.Life;

        _field.Add(ironhull);

        _field.DamageShip();

        int lifePosDamage = _field.Ship.Life;

        Assert.Greater(lifePreDamage, lifePosDamage);
    }

    [Test]
    public void MustRemoveShipWhenSunk()
    {
        var ironHull = new IronHull();

        _field.Add(ironHull);

        int life = ironHull.Life;

        for (int i = 0; i <= life; i++)
        {
            _field.DamageShip();
        }

        Assert.AreEqual(null, _field.Ship);
    }

    [Test]
    public void MustAddShip()
    {
        var ironHull = new IronHull();

        _field.Add(ironHull);

        Assert.AreEqual(ironHull, _field.Ship);
    }

    [Test]
    public void MustRaiseErrorWhenShipAlreadyOnField()
    {
        var ironHull = new IronHull();

        _field.Add(ironHull);

        Assert.Throws<ShipAlreadyExistsException>(Adicionar);

        void Adicionar()
        {
            _field.Add(ironHull);
        }
    }

    [Test]
    public void MustChangeShip()
    {
        var ironHull = new IronHull();
        var navalGuerrilla = new NavalGuerrilla();

        _field.Add(ironHull);

        _field.ChangeShip(navalGuerrilla);

        Assert.AreEqual(navalGuerrilla, _field.Ship);
    }

    [Test]
    public void MustRaiseErrorWhenChangingShipButThereIsNone()
    {
        var navalGuerrilla = new NavalGuerrilla();

        Assert.Throws<NoShipException>(ChangeShip);

        void ChangeShip()
        {
            _field.ChangeShip(navalGuerrilla);
        }
    }

    [Test]
    public void DeveAdicionarCanhao()
    {
        var canhao = new Cannon();

        _field.Add(canhao);

        Assert.AreEqual(canhao, _field.Cannons[0]);
    }

    [Test]
    public void MustAddCannons()
    {
        var cannons = new List<Cannon> {new(), new()};

        _field.Add(cannons);

        Assert.AreEqual(cannons, _field.Cannons);
    }

    [Test]
    public void DeveRemoverCanhao()
    {
        var cannon = new Cannon();

        _field.Add(cannon);

        _field.Remover(cannon);

        Assert.AreEqual(0, _field.Cannons.Count);
    }

    [Test]
    public void MustAddCrewMember()
    {
        var pirate = new Pirate();

        _field.Add(pirate);

        Assert.AreEqual(pirate, _field.Crew[0]);
    }

    [Test]
    public void MustRaiseErrorWhenAddingCrewMemberAndFull()
    {
        for (int i = 0; i < Field.MaximumCrew; i++)
        {
            AddPirate();
        }

        Assert.Throws<FullCrewException>(AddPirate);

        void AddPirate()
        {
            var pirate = new Pirate();

            _field.Add(pirate);
        }
    }

    [Test]
    public void MustRemoveCrewMember()
    {
        var pirate = new Pirate();

        _field.Add(pirate);
        _field.Remove(pirate);

        Assert.AreEqual(0, _field.Crew.Count);
    }

    [Test]
    public void MustRaiseErrorWhenRemovingNonExistantCrewMember()
    {
        Assert.Throws<EmptyCrewException>(Remove);

        void Remove()
        {
            var pirate = new Pirate();

            _field.Remove(pirate);
        }
    }

    [Test]
    public void MustRaiseErrorWhenDoesNotFindCrewMember()
    {
        var pirate = new Pirate();

        _field.Add(pirate);
        Assert.Throws<CrewMemberNotFoundException>(Remove);

        void Remove()
        {
            var pirateToRemove = new Pirate();

            _field.Remove(pirateToRemove);
        }
    }

    [Test]
    public void MustDrownCrew()
    {
        var pirate = new Pirate();

        _field.Add(pirate);

        _field.DrownCrew();

        Assert.AreEqual(0, _field.Crew.Count);
    }

    [Test]
    public void MustRemoveDuelCard()
    {
        var cannon = new Cannon();
        var surpriseAttack = new SurpriseAttack();

        _field.Add(cannon);
        _field.Add(surpriseAttack);

        _field.RemoveDuelCards();

        Assert.AreEqual(0, _field.Cannons.Count);
        Assert.AreEqual(0, _field.SurpriseDuel.Count);
    }

    [Test]
    public void MustAddProtectedCard()
    {
        var treasure = new Treasure(3);

        _field.AddProtected(treasure);

        Assert.AreEqual(treasure, _field.Protected[0]);
    }

    [Test]
    public void MustRemoveProtectedWhenDestroyingShip()
    {
        var treasure = new Treasure(3);
        var ironHull = new IronHull();

        _field.Add(ironHull);
        _field.AddProtected(treasure);

        int life = ironHull.Life;

        for (int i = 0; i <= life; i++)
        {
            _field.DamageShip();
        }

        Assert.AreEqual(null, _field.Ship);
    }

    [Test]
    public void MustAddSurpriseAttack()
    {
        var surpriseAttack = new SurpriseAttack();

        _field.Add(surpriseAttack);

        Assert.AreEqual(surpriseAttack, _field.SurpriseDuel[0]);
    }
}

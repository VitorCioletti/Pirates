namespace Pirates.Server.Domain.Test.Card.Ship;

using System.Collections.Generic;
using Action;
using Action.Resultant;
using Domain.Card.ImmediateResolution;
using Domain.Card.Ship;
using Domain.Card.Treasure;
using Exception.Card;
using NSubstitute;
using NUnit.Framework;

public class IronHullTests
{
    private Player _player;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("player1", (_, _) => { }, (_, _) => { }, (_, _) => { }, (_, _) => { });
    }

    [Test]
    public void ApplyEffectMustReturnChooseCardAtTheHandWithOnlyTreasuresAsOptions()
    {
        var treasure = new Treasure(3);
        var rum = new Rum();

        _player.Hand.Add(treasure);
        _player.Hand.Add(rum);

        var action = Substitute.For<BaseAction>(_player, null);
        var ironHull = new IronHull();

        List<BaseAction> resultantActions = ironHull.ApplyEffect(action, null);

        Assert.That(resultantActions.Count, Is.EqualTo(1));

        var chooseCardAtTheHand = resultantActions[0] as ChooseCardAtTheHand;

        Assert.That(chooseCardAtTheHand, Is.Not.Null);
        Assert.That(chooseCardAtTheHand.Starter, Is.EqualTo(_player));
        Assert.That(chooseCardAtTheHand.Options, Is.EqualTo(new List<string> {treasure.Id}));
    }

    [Test]
    public void ChoosingATreasureMustMoveItFromHandToProtectedField()
    {
        var treasure = new Treasure(3);

        _player.Hand.Add(treasure);

        var action = Substitute.For<BaseAction>(_player, null);
        var ironHull = new IronHull();

        var chooseCardAtTheHand = (ChooseCardAtTheHand) ironHull.ApplyEffect(action, null)[0];

        chooseCardAtTheHand.FillChoices(new List<string> {treasure.Id});

        List<BaseAction> result = chooseCardAtTheHand.ApplyRule(null);

        Assert.That(result, Is.Null);
        Assert.That(_player.Hand.Exists(treasure), Is.False);
        Assert.That(_player.Field.GetAllProtected().Contains(treasure), Is.True);
    }

    [Test]
    public void TakeDamageMustReduceLifeAndThrowWhenAlreadyDead()
    {
        var ironHull = new IronHull();

        ironHull.TakeDamage(2);
        Assert.That(ironHull.Life, Is.EqualTo(1));

        ironHull.TakeDamage(1);
        Assert.That(ironHull.Life, Is.EqualTo(0));

        Assert.Throws<ShipHasNoLifeException>(() => ironHull.TakeDamage(1));
    }

    [Test]
    public void FieldMustRemoveShipWhenLifeReachesZero()
    {
        var field = new Field();
        var ironHull = new IronHull();

        field.Add(ironHull);

        int initialLife = ironHull.Life;

        for (int i = 0; i < initialLife; i++)
            field.DamageShip();

        Assert.That(field.Ship, Is.Null);
    }
}

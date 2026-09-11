namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System.Collections.Generic;
using Action;
using Action.Resultant;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using NSubstitute;
using NUnit.Framework;

public class SpyglassTests
{
    private Player _starterPlayer;
    private Player _targetPlayer;

    [SetUp]
    public void SetUp()
    {
        _starterPlayer = new Player(
            "starter",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        _targetPlayer = new Player(
            "target",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });
    }

    [Test]
    public void ApplyEffectMustReturnDiscardCardWithAllOfTargetsHandCardIds()
    {
        var rum = new Rum();
        var sake = new Sake();

        _targetPlayer.Hand.Add(new List<Card> {rum, sake});

        var action = Substitute.For<BaseAction>(_starterPlayer, _targetPlayer);

        var spyglass = new Spyglass();

        List<BaseAction> result = spyglass.ApplyEffect(action, null);

        Assert.That(result.Count, Is.EqualTo(1));

        var discardCard = result[0] as DiscardCard;

        Assert.That(discardCard, Is.Not.Null);
        Assert.That(discardCard.Starter, Is.EqualTo(_starterPlayer));
        Assert.That(discardCard.Target, Is.EqualTo(_targetPlayer));
        Assert.That(discardCard.Options, Is.EquivalentTo(new List<string> {rum.Id, sake.Id}));
    }

    [Test]
    public void ApplyEffectMustReturnDiscardCardWithEmptyOptionsWhenTargetHandIsEmpty()
    {
        var action = Substitute.For<BaseAction>(_starterPlayer, _targetPlayer);

        var spyglass = new Spyglass();

        List<BaseAction> result = spyglass.ApplyEffect(action, null);

        Assert.That(result.Count, Is.EqualTo(1));

        var discardCard = result[0] as DiscardCard;

        Assert.That(discardCard, Is.Not.Null);
        Assert.That(discardCard.Options.Count, Is.EqualTo(0));
    }
}

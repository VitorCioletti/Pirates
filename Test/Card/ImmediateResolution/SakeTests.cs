namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System.Collections.Generic;
using Action;
using Domain.Card;
using Domain.Card.ImmediateResolution;
using Domain.Card.Passive;
using NSubstitute;
using NUnit.Framework;

public class SakeTests
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
    public void ApplyEffectMustStealCardFromTargetWhenTargetHasNoTrapChest()
    {
        var targetCard = Substitute.For<Card>();

        _targetPlayer.Hand.Add(targetCard);

        var action = Substitute.For<BaseAction>(_starterPlayer, _targetPlayer);

        var sake = new Sake();

        List<BaseAction> result = sake.ApplyEffect(action, null);

        Assert.IsNull(result);
        Assert.IsTrue(_starterPlayer.Hand.Exists(targetCard));
        Assert.IsFalse(_targetPlayer.Hand.Exists(targetCard));
    }

    [Test]
    public void ApplyEffectMustStealCardFromStarterWhenTargetHasTrapChest()
    {
        var starterCard = Substitute.For<Card>();
        var trapChest = new TrapChest();

        _starterPlayer.Hand.Add(starterCard);
        _targetPlayer.Hand.Add(trapChest);

        var action = Substitute.For<BaseAction>(_starterPlayer, _targetPlayer);

        var sake = new Sake();

        List<BaseAction> result = sake.ApplyEffect(action, null);

        Assert.IsNull(result);
        Assert.IsTrue(_targetPlayer.Hand.Exists(starterCard));
        Assert.IsFalse(_starterPlayer.Hand.Exists(starterCard));
        Assert.IsTrue(_targetPlayer.Hand.Exists(trapChest));
    }
}

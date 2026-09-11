namespace Pirates.Server.Domain.Test.Card.ImmediateResolution;

using System.Collections.Generic;
using Action;
using Action.Resultant;
using Domain.Card.Crew;
using Domain.Card.ImmediateResolution;
using Domain.Exception.Action;
using NSubstitute;
using NUnit.Framework;

public class ManOverboardTests
{
    [Test]
    public void ApplyEffectMustThrowDoesNotHaveCrewMemberExceptionWhenTargetFieldHasNoCrew()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var manOverboard = new ManOverboard();

        Assert.Throws<DoesNotHaveCrewMemberException>(() => manOverboard.ApplyEffect(action, null));
    }

    [Test]
    public void ApplyEffectMustThrowNoCrewMemberCanBeDrownedExceptionWhenTargetFieldCrewIsNotDrownable()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        targetPlayer.Field.Add(new GhostPirate());

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var manOverboard = new ManOverboard();

        Assert.Throws<NoCrewMemberCanBeDrownedException>(() => manOverboard.ApplyEffect(action, null));
    }

    [Test]
    public void ApplyEffectMustReturnDrownCrewMemberWhenTargetFieldHasDrownableCrew()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        targetPlayer.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var manOverboard = new ManOverboard();

        List<BaseAction> result = manOverboard.ApplyEffect(action, null);

        Assert.That(result.Count, Is.EqualTo(1));

        var drownCrewMember = result[0] as DrownCrewMember;

        Assert.That(drownCrewMember, Is.Not.Null);
        Assert.That(drownCrewMember.Starter, Is.EqualTo(starterPlayer));
        Assert.That(drownCrewMember.Target, Is.EqualTo(targetPlayer));
    }
}

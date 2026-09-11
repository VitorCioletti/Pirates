namespace Pirates.Server.Domain.Test.Card.Crew;

using Domain.Action;
using Domain.Card.Crew;
using Domain.Exception.Field;
using NSubstitute;
using NUnit.Framework;

public class GhostPirateTests
{
    [Test]
    public void ConstructorMustSetShotsAndDrownable()
    {
        var ghostPirate = new GhostPirate();

        Assert.AreEqual(0, ghostPirate.Shots);
        Assert.IsFalse(ghostPirate.Drownable);
    }

    [Test]
    public void ApplyEffectMustAddCardToTargetField()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var ghostPirate = new GhostPirate();

        var result = ghostPirate.ApplyEffect(action, null);

        Assert.IsTrue(targetPlayer.Field.Crew.Contains(ghostPirate));
        Assert.IsFalse(starterPlayer.Field.Crew.Contains(ghostPirate));
        Assert.IsNull(result);
    }

    [Test]
    public void ApplyEffectMustThrowWhenTargetFieldCrewIsFull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        targetPlayer.Field.Add(new GhostPirate());
        targetPlayer.Field.Add(new GhostPirate());

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var ghostPirate = new GhostPirate();

        Assert.Throws<FullCrewException>(() => ghostPirate.ApplyEffect(action, null));
    }

    [Test]
    public void DrownCrewMustNotRemoveGhostPirateFromField()
    {
        var player = new Player(string.Empty, null, null, null, null);
        var ghostPirate = new GhostPirate();

        player.Field.Add(ghostPirate);

        player.Field.DrownCrew();

        Assert.IsTrue(player.Field.Crew.Contains(ghostPirate));
    }
}

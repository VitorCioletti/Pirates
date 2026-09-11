namespace Pirates.Server.Domain.Test.Card.Crew;

using Domain.Action;
using Domain.Card.Crew;
using Domain.Exception.Field;
using NSubstitute;
using NUnit.Framework;

public class CursedPirateTests
{
    [Test]
    public void ConstructorMustSetShotsAndDrownable()
    {
        var cursedPirate = new CursedPirate();

        Assert.That(cursedPirate.Shots, Is.EqualTo(-1));
        Assert.That(cursedPirate.Drownable, Is.True);
    }

    [Test]
    public void ApplyEffectMustAddCardToTargetField()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var cursedPirate = new CursedPirate();

        var result = cursedPirate.ApplyEffect(action, null);

        Assert.That(targetPlayer.Field.Crew.Contains(cursedPirate), Is.True);
        Assert.That(starterPlayer.Field.Crew.Contains(cursedPirate), Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ApplyEffectMustThrowWhenTargetFieldCrewIsFull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        targetPlayer.Field.Add(new CursedPirate());
        targetPlayer.Field.Add(new CursedPirate());

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var cursedPirate = new CursedPirate();

        Assert.Throws<FullCrewException>(() => cursedPirate.ApplyEffect(action, null));
    }
}

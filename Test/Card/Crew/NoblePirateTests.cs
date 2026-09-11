namespace Pirates.Server.Domain.Test.Card.Crew;

using Domain.Action;
using Domain.Card.Crew;
using Domain.Exception.Field;
using NSubstitute;
using NUnit.Framework;

public class NoblePirateTests
{
    [Test]
    public void ConstructorMustSetShotsAndTreasures()
    {
        var noblePirate = new NoblePirate();

        Assert.That(noblePirate.Shots, Is.EqualTo(0));
        Assert.That(noblePirate.Treasures, Is.EqualTo(1));
        Assert.That(noblePirate.Drownable, Is.True);
    }

    [Test]
    public void ApplyEffectMustAddCardToStarterField()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var noblePirate = new NoblePirate();

        var result = noblePirate.ApplyEffect(action, null);

        Assert.That(starterPlayer.Field.Crew.Contains(noblePirate), Is.True);
        Assert.That(targetPlayer.Field.Crew.Contains(noblePirate), Is.False);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ApplyEffectMustThrowWhenStarterFieldCrewIsFull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        starterPlayer.Field.Add(new NoblePirate());
        starterPlayer.Field.Add(new NoblePirate());

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var noblePirate = new NoblePirate();

        Assert.Throws<FullCrewException>(() => noblePirate.ApplyEffect(action, null));
    }

    [Test]
    public void PlayerTreasurePointsMustCountEachNoblePirateOnField()
    {
        var player = new Player(string.Empty, null, null, null, null);

        player.Field.Add(new NoblePirate());
        player.Field.Add(new NoblePirate());

        Assert.That(player.CalculateTreasurePoints(), Is.EqualTo(2));
    }
}

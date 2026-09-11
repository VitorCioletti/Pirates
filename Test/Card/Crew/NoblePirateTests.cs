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

        Assert.AreEqual(0, noblePirate.Shots);
        Assert.AreEqual(1, noblePirate.Treasures);
        Assert.IsTrue(noblePirate.Drownable);
    }

    [Test]
    public void ApplyEffectMustAddCardToStarterField()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var noblePirate = new NoblePirate();

        var result = noblePirate.ApplyEffect(action, null);

        Assert.IsTrue(starterPlayer.Field.Crew.Contains(noblePirate));
        Assert.IsFalse(targetPlayer.Field.Crew.Contains(noblePirate));
        Assert.IsNull(result);
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

        Assert.AreEqual(2, player.CalculateTreasurePoints());
    }
}

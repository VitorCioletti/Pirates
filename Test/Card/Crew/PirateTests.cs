namespace Pirates.Server.Domain.Test.Card.Crew;

using Domain.Action;
using Domain.Card.Crew;
using Domain.Exception.Field;
using NSubstitute;
using NUnit.Framework;

public class PirateTests
{
    [Test]
    public void ConstructorMustSetDefaultShotsAndDrownable()
    {
        var pirate = new Pirate();

        Assert.AreEqual(0, pirate.Shots);
        Assert.IsTrue(pirate.Drownable);
    }

    [Test]
    public void ApplyEffectMustAddCardToStarterField()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);
        var targetPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, targetPlayer);
        var pirate = new Pirate();

        var result = pirate.ApplyEffect(action, null);

        Assert.IsTrue(starterPlayer.Field.Crew.Contains(pirate));
        Assert.IsFalse(targetPlayer.Field.Crew.Contains(pirate));
        Assert.IsNull(result);
    }

    [Test]
    public void ApplyEffectMustThrowWhenStarterFieldCrewIsFull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        starterPlayer.Field.Add(new Pirate());
        starterPlayer.Field.Add(new Pirate());

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var pirate = new Pirate();

        Assert.Throws<FullCrewException>(() => pirate.ApplyEffect(action, null));
    }
}

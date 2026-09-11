namespace Pirates.Server.Domain.Test.Card.Passive;

using Domain.Action;
using Domain.Card.Passive;
using NSubstitute;
using NUnit.Framework;

public class TrapChestTests
{
    [Test]
    public void ApplyEffectMustReturnNull()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var trapChest = new TrapChest();

        var result = trapChest.ApplyEffect(action, null);

        Assert.IsNull(result);
    }

    [Test]
    public void ApplyEffectMustNotModifyStarterFieldOrHand()
    {
        var starterPlayer = new Player(string.Empty, null, null, null, null);

        var action = Substitute.For<BaseAction>(starterPlayer, null);
        var trapChest = new TrapChest();

        trapChest.ApplyEffect(action, null);

        Assert.AreEqual(0, starterPlayer.Field.Crew.Count);
        Assert.AreEqual(0, starterPlayer.Hand.GetCardQuantity());
        Assert.IsNull(starterPlayer.Field.Ship);
    }
}

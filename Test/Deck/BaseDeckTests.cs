namespace Pirates.Server.Domain.Test;

using Deck;
using NSubstitute;
using NUnit.Framework;

public class BaseDeckTests
{
    private CentralDeck _deck;

    public BaseDeckTests()
    {
        CardsGenerator.Configure([]);
    }

    [SetUp]
    public void SetUp()
    {
        _deck = new CentralDeck();
        _deck.GenerateCards();
    }

    [Test]
    public void CardsAmountMustBeZeroForANewlyInitializedDeck()
    {
        Assert.That(_deck.CardsAmount, Is.EqualTo(0));
    }

    [Test]
    public void PushTopSingleCardMustAddItAndIncreaseCardsAmount()
    {
        var card = Substitute.For<Domain.Card.Card>();

        _deck.PushTop(card);

        Assert.That(_deck.CardsAmount, Is.EqualTo(1));
        Assert.That(_deck.GetTop(), Is.EqualTo(card));
    }

    [Test]
    public void PushTopWithAListOnAnEmptyDeckMustReverseTheListOrderWhenDrawing()
    {
        var firstCard = Substitute.For<Domain.Card.Card>();
        var secondCard = Substitute.For<Domain.Card.Card>();

        _deck.PushTop([firstCard, secondCard]);

        Assert.That(_deck.CardsAmount, Is.EqualTo(2));
        Assert.That(_deck.GetTop(), Is.EqualTo(secondCard));
        Assert.That(_deck.GetTop(), Is.EqualTo(firstCard));
    }

    [Test]
    public void PushBottomWithAListOnAnEmptyDeckMustPreserveTheListOrderWhenDrawing()
    {
        var firstCard = Substitute.For<Domain.Card.Card>();
        var secondCard = Substitute.For<Domain.Card.Card>();

        _deck.PushBottom([firstCard, secondCard]);

        Assert.That(_deck.CardsAmount, Is.EqualTo(2));
        Assert.That(_deck.GetTop(), Is.EqualTo(firstCard));
        Assert.That(_deck.GetTop(), Is.EqualTo(secondCard));
    }

    [Test]
    public void PushTopOnANonEmptyDeckMustPutTheNewCardAboveTheExistingOne()
    {
        var existingCard = Substitute.For<Domain.Card.Card>();
        var newlyPushedCard = Substitute.For<Domain.Card.Card>();

        _deck.PushTop(existingCard);
        _deck.PushTop(newlyPushedCard);

        Assert.That(_deck.GetTop(), Is.EqualTo(newlyPushedCard));
        Assert.That(_deck.GetTop(), Is.EqualTo(existingCard));
    }

    [Test]
    public void PushBottomOnANonEmptyDeckMustKeepTheNewCardBelowTheExistingOne()
    {
        var existingCard = Substitute.For<Domain.Card.Card>();
        var newlyPushedCard = Substitute.For<Domain.Card.Card>();

        _deck.PushTop(existingCard);
        _deck.PushBottom([newlyPushedCard]);

        Assert.That(_deck.GetTop(), Is.EqualTo(existingCard));
        Assert.That(_deck.GetTop(), Is.EqualTo(newlyPushedCard));
    }
}

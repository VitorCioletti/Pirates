namespace Pirates.Server.Domain.Test;

using System;
using System.Collections.Generic;
using System.Linq;
using Action;
using Action.Immediate;
using Action.Primary;
using Action.Resultant;
using Action.Resultant.Base;
using Action.Resultant.Enums;
using Deck;
using Domain.Card.Duel;
using Domain.Card.ImmediateResolution;
using Domain.Card.Treasure;
using Exception.Table;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Duel = Action.Primary.Duel;

public class TableTests
{
    private Table _table;

    public TableTests()
    {
        var cardsConfiguration = new List<Tuple<string, int>> {new(nameof(Rum), 100)};

        CardsGenerator.Configure(cardsConfiguration);
    }

    [SetUp]
    public void SetUp()
    {
        var players = new List<Player>();

        var player1 = new Player(
            "player1",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player2 = new Player(
            "player2",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        var player3 = new Player(
            "player3",
            (_, _) => { },
            (_, _) => { },
            (_, _) => { },
            (_, _) => { });

        players.Add(player1);
        players.Add(player2);
        players.Add(player3);

        _table = new Table(players);
    }

    [Test]
    public void AllPlayerMustHaveCardsWhenCreatingATable()
    {
        bool allHaveCards = _table.Players.All(j => j.Hand.GetCardQuantity() > 0);

        Assert.True(allHaveCards);
    }

    [Test]
    public void CentralDeckMustHaveCardsWhenCreatingATable()
    {
        bool hasCards = _table.CentralDeck.CardsAmount > 0;

        Assert.True(hasCards);
    }

    [Test]
    public void DiscardDeckMustBeEmptyWhenCreatingATable()
    {
        bool isEmpty = _table.DiscardDeck.CardsAmount == 0;

        Assert.True(isEmpty);
    }

    [Test]
    public void FirstPlayerMustHaveAvailablePrimaryActions()
    {
        var expectedAvailableActions = new List<BasePrimaryAction>
        {
            new Duel(null, null, null), new DrawCard(null, null), new BuyCard(null)
        };

        List<BaseAction> availableActions = _table.ActionsAvailableToPlayers[_table.CurrentPlayer];

        bool hasAllExpected =
            expectedAvailableActions.All(pe => availableActions.Exists(po => po.GetType() == pe.GetType()));

        Assert.IsTrue(hasAllExpected);
    }

    [Test]
    public void CurrentPlayerMustBeAbleToBuyACard()
    {
        Player currentPlayer = _table.CurrentPlayer;

        int cardsBeforeBuy = currentPlayer.Hand.GetCardQuantity();
        int availableActionsBeforeBuy = currentPlayer.AvailableActions;

        int expectedCards = cardsBeforeBuy + 1;
        int expectedAvailableActions = availableActionsBeforeBuy - 1;

        List<BaseAction> availableActions = _table.ActionsAvailableToPlayers[currentPlayer];

        BaseAction buyCard = availableActions.First(a => a is BuyCard);

        _table.ProcessAction(buyCard);

        Assert.AreEqual(expectedCards, currentPlayer.Hand.GetCardQuantity());
        Assert.AreEqual(expectedAvailableActions, currentPlayer.AvailableActions);
    }

    [Test]
    public void PlayerMustNotExecuteActionOutOfHisTurn()
    {
        Player nextPlayer = _table.Players[1];

        Assert.Throws<OtherPlayerTurnException>(ProcessAction);

        void ProcessAction()
        {
            _table.ProcessAction(new BuyCard(nextPlayer));
        }
    }

    [Test]
    public void PlayerMustNotExecuteNonExpectedResultant()
    {
        Player currentPlayer = _table.Players[0];

        var originAction = new DrawCard(currentPlayer, new Cannon());

        var resultant = new DiscardCard(
            originAction,
            currentPlayer,
            currentPlayer,
            new List<string>());

        Assert.Throws<UnexpectedResultantAction>(ProcessAction);

        void ProcessAction()
        {
            _table.ProcessAction(resultant);
        }
    }

    [Test]
    public void MustChangeCurrentPlayerAfterPreviousOnePlay()
    {
        foreach (Player player in _table.Players)
        {
            while (player.AvailableActions > 0)
            {
                List<BaseAction> availableActions = _table.ActionsAvailableToPlayers[player];

                BaseAction buyCard = availableActions.First(a => a is BuyCard);

                player.Hand.Remove(player.Hand.GetAny());

                _table.ProcessAction(buyCard);
            }
        }

        Assert.Pass();
    }

    [Test]
    public void PlayerMustWinIfHasEnoughTreasures()
    {
        Player currentPlayer = _table.CurrentPlayer;
        Player winnerPlayer = _table.Players[1];

        winnerPlayer.Hand.Add(new Treasure(5));

        while (currentPlayer.AvailableActions > 0)
        {
            List<BaseAction> availableActions = _table.ActionsAvailableToPlayers[currentPlayer];

            BaseAction buyCard = availableActions.First(a => a is BuyCard);

            currentPlayer.Hand.Remove(currentPlayer.Hand.GetAny());

            _table.ProcessAction(buyCard);
        }

        Assert.AreEqual(winnerPlayer, _table.Winner);
    }

    [Test]
    public void MustEnterInDuelMode()
    {
        Assert.IsFalse(_table.InDuel);

        _table.EnterDuelMode();

        Assert.IsTrue(_table.InDuel);
    }

    [Test]
    public void MustExitDuelMode()
    {
        _table.EnterDuelMode();

        Assert.IsTrue(_table.InDuel);

        _table.EndDuelMode();

        Assert.IsFalse(_table.InDuel);
    }

    [Test]
    public void MustThrowInDuelExceptionIfAlreadyInDuel()
    {
        _table.EnterDuelMode();

        Assert.Throws<InDuelException>(_table.EnterDuelMode);
    }

    [Test]
    public void MustThrowNoDuelExeptionIfNeverEntered()
    {
        Assert.Throws<NoDuelException>(_table.EndDuelMode);
    }

    [Test]
    public void CurrentPlayerMustBeAbleToExecutePrimaryAction()
    {
        Player currentPlayer = _table.CurrentPlayer;

        int cards = currentPlayer.Hand.GetCardQuantity();
        int availableActions = currentPlayer.AvailableActions;

        var buyCard = new BuyCard(currentPlayer);

        _table.ProcessAction(buyCard);

        Assert.IsTrue(cards < currentPlayer.Hand.GetCardQuantity());
        Assert.IsTrue(availableActions > currentPlayer.AvailableActions);
    }

    [Test]
    public void ExecuteAllPrimariesMustChangeCurrentPlayer()
    {
        Player currentPlayer = _table.CurrentPlayer;
        int currentTurn = _table.CurrentTurn;

        int availableActions = currentPlayer.AvailableActions;

        for (int i = 0; i < availableActions; i++)
        {
            var buyCard = new BuyCard(currentPlayer);

            _table.ProcessAction(buyCard);
        }

        Assert.AreNotEqual(currentPlayer, _table.CurrentPlayer);
        Assert.IsTrue(currentTurn < _table.CurrentTurn);
    }

    [Test]
    public void MustThrowErrorIfPlaysOutOfTurn()
    {
        Assert.Throws<OtherPlayerTurnException>(BuyCardOutOfTurn);

        void BuyCardOutOfTurn()
        {
            Player player = _table.Players[1];

            var buyCard = new BuyCard(player);

            _table.ProcessAction(buyCard);
        }
    }

    [Test]
    public void MustRegisterAndExecuteImmediateAction()
    {
        Player currentPlayer = _table.CurrentPlayer;

        bool primaryExecuted = false;
        bool immediateExecuted = false;

        var primary = Substitute.For<BasePrimaryAction>(currentPlayer, null);
        var immediate = Substitute.For<BaseImmediateAction>(currentPlayer, null);

        primary.When(i => i.ApplyRule(_table)).Do(OnApplyPrimaryRule);
        immediate.When(i => i.ApplyRule(_table)).Do(OnApplyImmediateRule);

        _table.ProcessAction(primary);

        Assert.IsTrue(primaryExecuted && immediateExecuted);

        void OnApplyPrimaryRule(CallInfo _)
        {
            _table.RegisterImmediateAfterResultants(immediate);

            primaryExecuted = true;
        }

        void OnApplyImmediateRule(CallInfo _)
        {
            immediateExecuted = true;
        }
    }

    [Test]
    public void PrimaryActionMustReturnResultantAction()
    {
        Player currentPlayer = _table.CurrentPlayer;

        bool primaryExecuted = false;
        bool resultantExecuted = false;

        var primary = Substitute.For<BasePrimaryAction>(currentPlayer, null);

        var resultant = Substitute.For<BaseResultant>(
            primary,
            currentPlayer,
            ChoiceType.Action,
            null);

        var expectedResultantActions = new List<BaseAction> {resultant};

        primary.ApplyRule(_table).Returns(expectedResultantActions).AndDoes(OnApplyPrimaryRule);
        resultant.When(i => i.ApplyRule(_table)).Do(OnApplyResultantRule);

        Dictionary<Player, List<BaseAction>> result = _table.ProcessAction(primary);

        Assert.IsTrue(result.Count > 0);

        BaseAction resultantAction = result[currentPlayer].Single();

        Assert.AreEqual(expectedResultantActions[0], resultantAction);

        Dictionary<Player, List<BaseAction>> resultantActionResult = _table.ProcessAction(resultantAction);

        Assert.IsTrue(resultantActionResult.Count == 0);
        Assert.IsTrue(primaryExecuted && resultantExecuted);

        void OnApplyPrimaryRule(CallInfo _)
        {
            primaryExecuted = true;
        }

        void OnApplyResultantRule(CallInfo _)
        {
            resultantExecuted = true;
        }
    }
}

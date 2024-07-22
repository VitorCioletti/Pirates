namespace Piratas.Servidor.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Passiva;
    using Acoes.Primaria;
    using Acoes.Resultante.Base;
    using Baralhos;
    using Cartas;
    using Cartas.Embarcacao;
    using Excecoes.Mesa;

    public class Table
    {
        public Guid Id { get; private set; }

        public List<Player> Players { get; }

        public Player CurrentPlayer { get; private set; }

        public CentralDeck CentralDeck { get; }

        public DiscardDeck DiscardDeck { get; private set; }

        public Stack<BaseAction> ActionHistory { get; }

        public int CurrentTurn { get; private set; }

        public Dictionary<Player, List<BaseAction>> ActionsAvailableToPlayers { get; private set; }

        public Player Winner { get; private set; }

        public bool InDuel { get; private set; }

        private Queue<Player> _playOrder { get; }

        private BaseImmediateAction _immediateAfterAllResultants;

        private readonly List<BaseAction> _pendingActions;

        private const int _initialCardsPerPlayer = 5;

        private const int _treasuresToWin = 5;

        private const int _actionsPerTurn = 3;

        public Table(List<Player> players)
        {
            _pendingActions = new List<BaseAction>();
            ActionsAvailableToPlayers = new Dictionary<Player, List<BaseAction>>();
            _immediateAfterAllResultants = null;

            Id = Guid.NewGuid();

            ActionHistory = new Stack<BaseAction>();
            CentralDeck = new CentralDeck();
            DiscardDeck = new DiscardDeck();

            Players = players;
            _playOrder = _generateOrderToPlay();
            CurrentPlayer = _getNextPlayer();

            ActionsAvailableToPlayers[CurrentPlayer] = _getPrimaryActions();

            foreach (Player player in Players)
                player.ResetAvailableActions(_actionsPerTurn);

            CentralDeck.GenerateCards();

            _ditributeCards();
        }

        public Dictionary<Player, List<BaseAction>> ProcessAction(BaseAction action)
        {
            action.Turn = CurrentTurn;
            Player starter = action.Starter;

            var actionsPerPlayer = new Dictionary<Player, List<BaseAction>>();

            _checkPrimaryCurrentPlayer(action);
            _verifyPendingResultant(action);

            List<BaseAction> actions = action.ApplyRule(this);

            ActionHistory.Push(action);

            if (actions != null)
            {
                List<BaseAction> immediateActions =
                    actions.Where(a => a is BaseImmediateAction).ToList();

                if (immediateActions.Count > 0)
                    _processImmediateActions(immediateActions, actionsPerPlayer);
            }

            if (action is BasePrimaryAction)
                starter.SubtractAvailableActions();

            if (_pendingActions.Count == 0 && _immediateAfterAllResultants != null)
            {
                _processImmediateAction(_immediateAfterAllResultants, actionsPerPlayer);
            }

            bool doesNotHaveActionResult =
                actions is null || actions.Count == 0;

            bool doesNotHaveAvailableActions = CurrentPlayer.AvailableActions == 0;

            if (doesNotHaveActionResult && doesNotHaveAvailableActions)
            {
                actionsPerPlayer = _moveToNextTurn();
            }
            else if (actions is not null)
            {
                Player player = actions[0].Starter;

                actionsPerPlayer.Add(player, actions);
            }

            if (actionsPerPlayer.Count > 0)
            {
                foreach (List<BaseAction> pendingActions in actionsPerPlayer.Values)
                    _pendingActions.AddRange(pendingActions);
            }

            return actionsPerPlayer;
        }

        private List<BaseAction> _getPrimaryActions()
        {
            var acoes = new List<BaseAction>
            {
                new Duel(CurrentPlayer, null, null), new DrawCard(CurrentPlayer, null), new BuyCard(CurrentPlayer)
            };

            return acoes;
        }

        private void _processImmediateAction(
            BaseImmediateAction immediateAction,
            IReadOnlyDictionary<Player, List<BaseAction>> actionsPerPlayer)
        {
            _immediateAfterAllResultants = null;
            _processImmediateActions(new List<BaseImmediateAction> {immediateAction}, actionsPerPlayer);
        }

        private void _processImmediateActions(
            IEnumerable<BaseAction> resultantActions,
            IReadOnlyDictionary<Player, List<BaseAction>> actionsPerPlayer)
        {
            IEnumerable<BaseImmediateAction>
                immediateActions = resultantActions.OfType<BaseImmediateAction>();

            foreach (BaseImmediateAction immediateAction in immediateActions)
            {
                Dictionary<Player, List<BaseAction>> actionsAfterImmediate = ProcessAction(immediateAction);

                foreach ((Player player, List<BaseAction> action) in actionsAfterImmediate)
                    actionsPerPlayer[player].AddRange(action);
            }
        }

        private Dictionary<Player, List<BaseAction>> _moveToNextTurn()
        {
            if (CurrentPlayer?.AvailableActions > 0)
                throw new HasAvailableActionsException(CurrentPlayer);

            CurrentTurn++;

            Player nextPlayer = _getNextPlayer();

            var actionsAfterShipEffects = new Dictionary<Player, List<BaseAction>>();

            if (nextPlayer.CalculateTreasurePoints() >= _treasuresToWin)
            {
                EndGame(nextPlayer);

                return actionsAfterShipEffects;
            }

            BaseShip ship = nextPlayer.Field.Ship;

            if (ship != null)
            {
                var applyShipEffect = new ApplyShipEffect(nextPlayer, ship);
                actionsAfterShipEffects = ProcessAction(applyShipEffect);
            }

            nextPlayer.ResetAvailableActions(_actionsPerTurn);

            return actionsAfterShipEffects;
        }

        public void EnterDuelMode()
        {
            if (InDuel)
                throw new InDuelException();

            InDuel = true;
        }

        public void EndDuelMode()
        {
            if (!InDuel)
                throw new NoDuelException();

            InDuel = false;
        }

        public void EndGame(Player vencedor)
        {
            Winner = vencedor;
        }

        public void RegisterImmediateAfterResultants(BaseImmediateAction imediate)
        {
            if (_immediateAfterAllResultants != null)
                throw new ImmediateAlreadyRegisteredException();

            _immediateAfterAllResultants = imediate;
        }

        private Queue<Player> _generateOrderToPlay() => new(Players);

        private Player _getNextPlayer()
        {
            Player nextPlayer = _playOrder.Dequeue();
            _playOrder.Enqueue(nextPlayer);

            CurrentPlayer = nextPlayer;
            ActionsAvailableToPlayers[CurrentPlayer] = _getPrimaryActions();

            return nextPlayer;
        }

        private void _ditributeCards()
        {
            foreach (Player player in Players)
            {
                List<Card> cards = CentralDeck.GetTop(_initialCardsPerPlayer);

                player.Hand.Add(cards);
            }
        }

        private void _checkPrimaryCurrentPlayer(BaseAction action)
        {
            Player starter = action.Starter;

            if (action is not BasePrimaryAction)
                return;

            if (starter != CurrentPlayer)
                throw new OtherPlayerTurnException(starter);
        }

        private void _verifyPendingResultant(BaseAction action)
        {
            if (action is not BaseResultant resultant)
                return;

            if (!_pendingActions.Contains(resultant))
                throw new UnexpectedResultantAction(resultant);
        }
    }
}

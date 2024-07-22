namespace Piratas.Servidor.Servico.Partida
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configuracao;
    using Dominio;
    using Dominio.Acoes;
    using Dominio.Acoes.Primaria;
    using Dominio.Acoes.Resultante.Base;
    using Dominio.Baralhos;
    using Dominio.Cartas;
    using Dominio.Excecoes;
    using Excecoes.Partida;
    using Microsoft.Extensions.Configuration;
    using Protocolo;
    using Protocolo.Partida;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Cliente.Escolha;
    using Protocolo.Partida.Servidor;
    using Protocolo.Partida.Servidor.Escolha;

    internal sealed class MatchService
    {
        public Guid Id { get; }

        private readonly Table _table;

        private readonly Dictionary<Player, List<BaseAction>> _availableActionsSentToPlayers;

        private readonly Dictionary<string, List<Event>> _currentActionsTriggeredEvents;

        private readonly object _lockObject;

        public MatchService(List<string> playersIds)
        {
            _currentActionsTriggeredEvents = new Dictionary<string, List<Event>>();

            var players = new List<Player>();

            foreach (string playerId in playersIds)
            {
                var player = new Player(
                    playerId,
                    _onAddCardAtHand,
                    _onRemoveCardAtHand,
                    _onAddCardAtField,
                    _onRemoveCardAtField);

                players.Add(player);

                _currentActionsTriggeredEvents[player.Id] = new List<Event>();
            }

            _lockObject = new object();

            _table = new Table(players);

            Id = _table.Id;

            _availableActionsSentToPlayers = new Dictionary<Player, List<BaseAction>>();
        }

        public static void ConfigureCardGenerator()
        {
            var cardConfiguration = new List<Tuple<string, int>>();

            IConfigurationSection deck = ConfigurationService.Data.GetSection("Deck");

            IEnumerable<IConfigurationSection> cardsTypes = deck.GetChildren();

            foreach (IConfigurationSection cardType in cardsTypes)
            {
                IEnumerable<IConfigurationSection> cards = cardType.GetChildren();

                foreach (IConfigurationSection card in cards)
                {
                    string cardName = card.Key;
                    string cardAmount = card.Value;

                    if (int.TryParse(cardAmount, out int amount))
                    {
                        var configuration = new Tuple<string, int>(cardName, amount);

                        cardConfiguration.Add(configuration);
                    }
                    else
                        throw new InvalidOperationException($"Card \"{cardName}\" misconfigured.");
                }
            }

            CardsGenerator.Configure(cardConfiguration);
        }

        public List<ServerMatchMessage> ProcessClientMessage(ClientMatchMessage clientMatchMessage)
        {
            lock (_lockObject)
                return _processClientMessage(clientMatchMessage);
        }

        private List<ServerMatchMessage> _processClientMessage(ClientMatchMessage clientMatchMessage)
        {
            var serverMessages = new List<ServerMatchMessage>();

            try
            {
                switch (clientMatchMessage.MessageType)
                {
                    case ClientMessageType.MatchCurrentState:
                        return _getMatchCurrentState();

                    case ClientMessageType.Choice:
                        Player playerWithPendingAction = _getPlayerWithPendingAction(clientMatchMessage);
                        BaseAction pendingAction = _getPendingAction(playerWithPendingAction, clientMatchMessage);

                        Dictionary<Player, List<BaseAction>> resultantActions = _table.ProcessAction(pendingAction);

                        foreach ((Player player, List<BaseAction> availableActions) in resultantActions)
                        {
                            ServerMatchMessage serverMatchMessage =
                                _createServerMessage(player, availableActions);

                            serverMessages.Add(serverMatchMessage);

                            _currentActionsTriggeredEvents.Clear();
                            _availableActionsSentToPlayers.Add(player, availableActions);
                        }

                        _availableActionsSentToPlayers[playerWithPendingAction].Remove(pendingAction);

                        break;

                    default:
                        throw new MessageTypeNotSupported();
                }
            }
            catch (BaseServiceException serviceException)
            {
                serverMessages.Add(new ServerMatchMessage(serviceException.Id, serviceException.Message));
            }
            catch (BaseDomainException domainException)
            {
                serverMessages.Add(new ServerMatchMessage(domainException.Id, domainException.Message));
            }

            return serverMessages;
        }

        private BaseAction _getPendingAction(Player player, ClientMatchMessage clientMatchMessage)
        {
            string executedActionId = clientMatchMessage.ExecutedActionId;

            List<BaseAction> pendingActions = _availableActionsSentToPlayers[player];

            BaseAction pendingAction = pendingActions.FirstOrDefault(a => a.Id == executedActionId);

            if (pendingAction == null)
                throw new NotAvailableActionException(executedActionId);

            switch (pendingAction)
            {
                case BaseResultantWithDictionaryChoice resultantWithDictionaryChoice:
                    Dictionary<string, string> choiceDictionary =
                        ((ClientBooleanChoiceDictionary)clientMatchMessage.Choice).Choices;

                    resultantWithDictionaryChoice.FillChoices(choiceDictionary);

                    break;

                case BaseResultantWithBooleanChoice resultantWithBooleanChoice:
                    bool booleanChoice = ((ClientOneBooleanChoice)clientMatchMessage.Choice).Choice;

                    resultantWithBooleanChoice.FillChoice(booleanChoice);

                    break;

                case BaseResultantWithChoiceList resultantWithChoiceList:
                    List<string> listChoice = ((ClientChoiceList)clientMatchMessage.Choice).Choices;

                    resultantWithChoiceList.FillChoices(listChoice);

                    break;
            }

            return pendingAction;
        }

        private Player _getPlayerWithPendingAction(ClientMatchMessage clientMatchMessage)
        {
            string starterPlayerId = clientMatchMessage.IdStarterPlayer;

            (Player playerWithPendingAction, List<BaseAction> _) =
                _availableActionsSentToPlayers.FirstOrDefault(a => a.Key.Id == starterPlayerId);

            if (playerWithPendingAction == null)
                throw new PlayerWithoutPendingActionException(starterPlayerId);

            return playerWithPendingAction;
        }

        private ServerMatchMessage _createServerMessage(Player player, List<BaseAction> actions)
        {
            BaseChoice choice = _createChoice(actions);

            var serverMatchMessage = new ServerMatchMessage(
                player.Id,
                Id,
                player.AvailableActions,
                player.CalculateTreasurePoints(),
                _currentActionsTriggeredEvents,
                choice,
                _table.CurrentPlayer.Id,
                string.Empty);

            return serverMatchMessage;
        }

        private BaseChoice _createChoice(List<BaseAction> actions)
        {
            if (actions.Count == 0)
                return null;

            if (actions.All(a => a is BasePrimaryAction))
            {
                List<string> actionsIds = actions.Select(a => a.Id.ToString()).ToList();

                var serverChoicesLIst = new ServerChoicesLIst(ChoiceType.Action, actionsIds);

                return serverChoicesLIst;
            }

            var resultant = (BaseResultant)actions[0];

            BaseChoice choice = _createResultantActionChoice(resultant);

            return choice;
        }

        private BaseChoice _createResultantActionChoice(BaseResultant resultant)
        {
            BaseChoice resultantChoice;

            switch (resultant)
            {
                case BaseResultantWithDictionaryChoice resultantWithDictionaryChoice:
                    resultantChoice = new ServerDictionaryChoices(
                        _getProtocolChoiceType(resultantWithDictionaryChoice.ChoiceType),
                        _getProtocolChoiceType(resultantWithDictionaryChoice.ChoiceKeyType),
                        _getProtocolChoiceType(resultantWithDictionaryChoice.ChoiceKeyOption),
                        resultantWithDictionaryChoice.LimitValuePerKey,
                        resultantWithDictionaryChoice.KeysOptions,
                        resultantWithDictionaryChoice.ValueOptions);

                    break;

                case BaseResultantWithBooleanChoice resultantWithBooleanChoice:
                    resultantChoice =
                        new ServerOneBooleanChoice(_getProtocolChoiceType(resultantWithBooleanChoice.ChoiceType));

                    break;

                case BaseResultantWithChoiceList resultantWithChoiceList:
                    resultantChoice = new ServerChoicesLIst(
                        _getProtocolChoiceType(resultantWithChoiceList.ChoiceType),
                        resultantWithChoiceList.Options,
                        resultantWithChoiceList.ChoiceLimit);

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(resultant));
            }

            return resultantChoice;
        }

        private ChoiceType _getProtocolChoiceType(Dominio.Acoes.Resultante.Enums.ChoiceType choiceType)
        {
            return choiceType switch
            {
                Dominio.Acoes.Resultante.Enums.ChoiceType.Action => ChoiceType.Action,
                Dominio.Acoes.Resultante.Enums.ChoiceType.Player => ChoiceType.Player,
                Dominio.Acoes.Resultante.Enums.ChoiceType.Card => ChoiceType.Card,
                _ => throw new ArgumentOutOfRangeException(nameof(choiceType), choiceType, null)
            };
        }

        private void _onAddCardAtHand(string playerId, Card card)
        {
            _adicionarEvento(
                playerId,
                EventLocation.Hand,
                card.Id,
                true);
        }

        private void _onRemoveCardAtHand(string playerId, Card card)
        {
            _adicionarEvento(
                playerId,
                EventLocation.Hand,
                card.Id,
                false);
        }

        private void _onAddCardAtField(string playerId, Card card)
        {
            _adicionarEvento(
                playerId,
                EventLocation.Field,
                card.Id,
                true);
        }

        private void _onRemoveCardAtField(string playerId, Card card)
        {
            _adicionarEvento(
                playerId,
                EventLocation.Field,
                card.Id,
                false);
        }

        private void _adicionarEvento(
            string playerId,
            EventLocation eventLocation,
            string cardId,
            bool added)
        {
            var newEvent = new Event(eventLocation, cardId, added);

            _currentActionsTriggeredEvents[playerId].Add(newEvent);
        }

        private List<ServerMatchMessage> _getMatchCurrentState()
        {
            var messages = new List<ServerMatchMessage>();

            foreach (Player player in _table.Players)
            {
                List<BaseAction> availableActions = _table.ActionsAvailableToPlayers[player];

                List<string> idAvailableActions = availableActions.Select(a => a.Id).ToList();

                var choice = new ClientChoiceList(ChoiceType.Action, idAvailableActions);

                Dictionary<string, List<Event>> events = _getCurrentMatchState(player);

                var serverMatchMessage = new ServerMatchMessage(
                    player.Id,
                    _table.Id,
                    player.AvailableActions,
                    player.CalculateTreasurePoints(),
                    events,
                    choice,
                    _table.CurrentPlayer.Id);

                messages.Add(serverMatchMessage);
            }

            return messages;
        }

        private Dictionary<string, List<Event>> _getCurrentMatchState(Player target)
        {
            var allEvents = new Dictionary<string, List<Event>>();

            foreach (Player player in _table.Players)
            {
                var eventsAddedCards = new List<Event>();

                IEnumerable<Event> handEvents = _createEvents(
                    player,
                    target,
                    EventLocation.Hand,
                    player.Hand.GetAll());

                IEnumerable<Event> cannonEvents = _createEvents(
                    player,
                    target,
                    EventLocation.Cannon,
                    player.Field.Cannons);

                IEnumerable<Event> protectedEvents = _createEvents(
                    player,
                    target,
                    EventLocation.Protected,
                    player.Field.Protected);

                IEnumerable<Event> surpriseDuelEvents = _createEvents(
                    player,
                    target,
                    EventLocation.SurpriseDuel,
                    player.Field.SurpriseDuel);

                IEnumerable<Event> crewEvents = _createEvents(
                    player,
                    target,
                    EventLocation.Crew,
                    player.Field.Crew);

                IEnumerable<Event> shipEvents = _createEvents(
                    player,
                    target,
                    EventLocation.Ship,
                    new List<Card> {player.Field.Ship});

                eventsAddedCards.AddRange(handEvents);
                eventsAddedCards.AddRange(cannonEvents);
                eventsAddedCards.AddRange(protectedEvents);
                eventsAddedCards.AddRange(surpriseDuelEvents);
                eventsAddedCards.AddRange(crewEvents);
                eventsAddedCards.AddRange(shipEvents);

                allEvents[player.Id] = eventsAddedCards;
            }

            return allEvents;
        }

        private IEnumerable<Event> _createEvents(
            Player player,
            Player target,
            EventLocation eventLocation,
            IEnumerable<Card> cards)
        {
            var addedCardsEvents = new List<Event>();

            foreach (Card card in cards)
            {
                bool shouldShowCardId = player.Id == target.Id;

                string cardId = shouldShowCardId ? card.Id : string.Empty;

                var newEvent = new Event(eventLocation, cardId, true);

                addedCardsEvents.Add(newEvent);
            }

            return addedCardsEvents;
        }
    }
}

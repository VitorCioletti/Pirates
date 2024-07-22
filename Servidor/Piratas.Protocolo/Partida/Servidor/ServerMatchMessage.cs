namespace Piratas.Protocolo.Partida.Servidor
{
    using System;
    using System.Collections.Generic;

    public class ServerMatchMessage : MatchMessage
    {
        public int RemainingActions { get; private set; }

        public int TreasurePoints { get; private set; }

        public BaseChoice Choice { get; private set; }

        public Dictionary<string, List<Event>> Events { get; private set; }

        public string CurrentTurnPlayerId { get; private set; }

        public ServerMatchMessage(
            string idStarterPlayerId,
            Guid roomId,
            int remainingActions,
            int treasurePoints,
            Dictionary<string, List<Event>> events,
            BaseChoice choice,
            string currentTurnPlayerId,
            string errorId = null,
            string errorDescription = null
        ) : base(
            idStarterPlayerId,
            roomId,
            errorId,
            errorDescription)
        {
            Choice = choice;
            CurrentTurnPlayerId = currentTurnPlayerId;
            RemainingActions = remainingActions;
            Events = events;
            TreasurePoints = treasurePoints;
        }

        public ServerMatchMessage(string errorId, string errorDescription) : this(
            string.Empty,
            Guid.Empty,
            0,
            0,
            null,
            null,
            string.Empty,
            errorId,
            errorDescription)
        {
        }
    }
}

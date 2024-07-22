namespace Piratas.Protocolo.Sala.Servidor
{
    using System;
    using System.Collections.Generic;

    public class RoomServerMessage : Message
    {
        public Guid RoomId { get; private set; }

        public Guid MatchId { get; private set; }

        public string StarterPlayerId { get; private set; }

        public List<string> Players { get; private set; }

        public RoomServerMessage(
            Guid roomId,
            string starterPlayerId,
            Guid matchId,
            List<string> players,
            string errorId = null,
            string errorDescription = null) : base(errorId, errorDescription)
        {
            RoomId = roomId;
            StarterPlayerId = starterPlayerId;
            MatchId = matchId;
            Players = players;
        }

        public RoomServerMessage(
            List<string> players,
            string errorId = null,
            string errorDescription = null) : base(errorId, errorDescription)
        {
            Players = players;
            MatchId = Guid.Empty;
            RoomId = Guid.Empty;
            StarterPlayerId = String.Empty;
        }
    }
}

namespace Piratas.Protocolo
{
    using System;

    public abstract class MatchMessage : Message
    {
        public Guid RoomId { get; private set; }

        public string IdStarterPlayer { get; private set; }

        public DateTime DateTime { get; private set; }

        protected MatchMessage(
            string idStarterPlayerId,
            Guid roomId,
            string errorId = null,
            string errorDescription = null) : base(errorId, errorDescription)
        {
            IdStarterPlayer = idStarterPlayerId;
            RoomId = roomId;
            DateTime = DateTime.UtcNow;
        }
    }
}

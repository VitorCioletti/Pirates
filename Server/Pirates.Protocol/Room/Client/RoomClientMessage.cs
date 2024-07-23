namespace Pirates.Protocol.Room.Client
{
    using System;

    public class RoomClientMessage : Message
    {
        public Guid StarterPlayerId { get; private set; }

        public RoomOperationType RoomOperationType { get; private set; }

        public Guid RoomId { get; private set; }

        public RoomClientMessage(Guid starterPlayerId, Guid roomId, RoomOperationType roomOperationType)
        {
            RoomId = roomId;
            StarterPlayerId = starterPlayerId;

            RoomOperationType = roomOperationType;
        }
    }
}

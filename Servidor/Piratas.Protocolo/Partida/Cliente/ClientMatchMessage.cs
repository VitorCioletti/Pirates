namespace Piratas.Protocolo.Partida.Cliente
{
    using System;

    public class ClientMatchMessage : MatchMessage
    {
        public ClientMessageType MessageType { get; private set; }

        public BaseChoice Choice { get; private set; }

        public string ExecutedActionId { get; private set; }

        public ClientMatchMessage(
            string idStarterPlayerId,
            Guid roomId,
            string executedActionId,
            BaseChoice choice,
            ClientMessageType messageType)
            : base(idStarterPlayerId, roomId)
        {
            ExecutedActionId = executedActionId;
            Choice = choice;
            MessageType = messageType;
        }
    }
}

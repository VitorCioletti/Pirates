namespace Piratas.Protocolo
{
    using System;

    public class Message
    {
        public Guid Id { get; private set; }

        public string ErrorId { get; private set; }

        public string ErrorDescription { get; private set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorId);

        public Message(string errorId = null, string errorDescription = null)
        {
            Id = Guid.NewGuid();
            ErrorId = errorId;
            ErrorDescription = errorDescription;
        }
    }
}

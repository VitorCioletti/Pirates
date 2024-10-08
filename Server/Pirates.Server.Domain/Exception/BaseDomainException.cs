namespace Pirates.Server.Domain.Exception
{
    using System;

    public abstract class BaseDomainException : Exception
    {
        public string Id { get; private set; }

        protected BaseDomainException(string id, string message) : base(message)
        {
            Id = id;
        }
    }
}

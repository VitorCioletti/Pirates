namespace Piratas.Protocolo
{
    using System;

    public abstract class BaseParserException : Exception
    {
        public string Id { get; private set; }

        protected BaseParserException(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

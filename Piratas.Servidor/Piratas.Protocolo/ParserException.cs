namespace Piratas.Protocolo
{
    using System;

    public class ParserException : Exception
    {
        public string Id { get; private set; }

        public ParserException(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

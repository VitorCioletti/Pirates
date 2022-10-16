namespace Piratas.Protocolo.Excecoes
{
    using System;

    public abstract class BaseParserExcecao : Exception
    {
        public string Id { get; private set; }

        protected BaseParserExcecao(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

namespace Piratas.Protocolo.Excecoes
{
    using System;

    public class SerializacaoException : BaseParserException
    {
        public SerializacaoException(Exception exception) :
            base("erro-serializacao", $"Erro de serialização \"{exception}\".")
        {
        }
    }
}

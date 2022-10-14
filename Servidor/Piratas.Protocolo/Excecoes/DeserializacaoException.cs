namespace Piratas.Protocolo.Excecoes
{
    using System;

    public class DeserializacaoException : BaseParserException
    {
        public DeserializacaoException(Exception exception) :
            base("erro-deserializacao", $"Erro de deserialização \"{exception}\".")
        {
        }
    }
}

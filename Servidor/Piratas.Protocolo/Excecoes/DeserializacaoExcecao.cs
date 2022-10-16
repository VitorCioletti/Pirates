namespace Piratas.Protocolo.Excecoes
{
    using System;

    public class DeserializacaoExcecao : BaseParserExcecao
    {
        public DeserializacaoExcecao(Exception exception) :
            base("erro-deserializacao", $"Erro de deserialização \"{exception}\".")
        {
        }
    }
}

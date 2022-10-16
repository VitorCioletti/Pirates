namespace Piratas.Protocolo.Excecoes
{
    using System;

    public class SerializacaoExcecao : BaseParserExcecao
    {
        public SerializacaoExcecao(Exception exception) :
            base("erro-serializacao", $"Erro de serialização \"{exception}\".")
        {
        }
    }
}

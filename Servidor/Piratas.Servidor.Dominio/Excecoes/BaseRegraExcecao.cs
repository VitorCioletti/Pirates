namespace Piratas.Servidor.Dominio.Excecoes
{
    using System;

    public abstract class BaseRegraExcecao : Exception
    {
        public string Id { get; private set; }

        protected BaseRegraExcecao(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

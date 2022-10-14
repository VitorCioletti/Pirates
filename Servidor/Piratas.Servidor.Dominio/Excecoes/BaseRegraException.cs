namespace Piratas.Servidor.Dominio.Excecoes
{
    using System;

    public abstract class BaseRegraException : Exception
    {
        public string Id { get; private set; }

        public BaseRegraException(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

namespace Piratas.Servidor.Servico
{
    using System;

    public abstract class BaseServicoException : Exception
    {
        public string Id { get; private set; }

        protected BaseServicoException(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

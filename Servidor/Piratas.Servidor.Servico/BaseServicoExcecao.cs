namespace Piratas.Servidor.Servico
{
    using System;

    public abstract class BaseServicoExcecao : Exception
    {
        public string Id { get; private set; }

        protected BaseServicoExcecao(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

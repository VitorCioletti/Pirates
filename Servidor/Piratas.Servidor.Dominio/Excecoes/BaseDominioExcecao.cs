namespace Piratas.Servidor.Dominio.Excecoes
{
    using System;

    public abstract class BaseDominioExcecao : Exception
    {
        public string Id { get; private set; }

        protected BaseDominioExcecao(string id, string mensagem) : base(mensagem)
        {
            Id = id;
        }
    }
}

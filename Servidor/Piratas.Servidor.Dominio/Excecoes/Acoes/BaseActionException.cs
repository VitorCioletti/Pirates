namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public abstract class BaseActionException : BaseDomainException
    {
        public BaseAction Action { get; private set; }

        protected BaseActionException(BaseAction action, string id, string message) : base(id, message)
        {
            Action = action;
        }
    }
}

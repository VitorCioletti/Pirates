namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public abstract class BaseTableException : BaseDomainException
    {
        protected BaseTableException(string id, string message) : base(id, message)
        {
        }
    }
}

namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public abstract class BaseFieldException : BaseDomainException
    {
        protected BaseFieldException(string id, string message) : base(id, message)
        {
        }
    }
}

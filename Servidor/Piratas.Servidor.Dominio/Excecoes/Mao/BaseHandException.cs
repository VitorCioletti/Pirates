namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public abstract class BaseHandException : BaseDomainException
    {
        protected BaseHandException(string id, string message) : base(id, message)
        {
        }
    }
}

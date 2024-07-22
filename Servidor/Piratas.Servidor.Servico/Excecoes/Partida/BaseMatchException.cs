namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public abstract class BaseMatchException : BaseServiceException
    {
        protected BaseMatchException(string id, string message) : base(id, message)
        {
        }
    }
}

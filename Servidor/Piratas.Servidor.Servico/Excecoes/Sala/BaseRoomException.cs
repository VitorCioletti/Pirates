namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    public abstract class BaseRoomException : BaseServiceException
    {
        protected BaseRoomException(string id, string message) : base(id, message)
        {
        }
    }
}

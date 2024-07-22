namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class MessageTypeNotSupported : BaseServiceException
    {
        public MessageTypeNotSupported() :
            base("message-type-not-supported", "Message type not supported.")
        {
        }
    }
}

namespace Piratas.Servidor.Servico.WebSocket
{
    using Protocolo;
    using Protocolo.Cliente;
    using Protocolo.Servidor;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    // TODO: Responsável por obter mensagens de criação, entrada e saída de sala.
    public class SalaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemCliente mensagemCliente = Parser.Deserializa<MensagemCliente>(e.Data);
            }
            catch (ParserException parserException)
            {
                var mensagem = new MensagemServidor(parserException.Id);
                var mensagemSerializada = Parser.Serializa(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

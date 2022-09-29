namespace Piratas.Servidor.Servico.WebSocket
{
    using Protocolo;
    using Protocolo.Servidor;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    internal class PartidaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                Mensagem mensagem = Parser.Deserializa(e.Data);
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

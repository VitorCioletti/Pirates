namespace Piratas.Servidor.Servico.WebSocket
{
    using Protocolo;
    using Protocolo.Cliente.Partida;
    using Protocolo.Servidor.Partida;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    // TODO: Responsável por obter mensagens de criação, entrada e saída de sala.
    public class SalaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemPartidaCliente mensagemCliente = Parser.Deserializa<MensagemPartidaCliente>(e.Data);
            }
            catch (ParserException parserException)
            {
                var mensagem = new MensagemPartidaServidor(parserException.Id);
                var mensagemSerializada = Parser.Serializa(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

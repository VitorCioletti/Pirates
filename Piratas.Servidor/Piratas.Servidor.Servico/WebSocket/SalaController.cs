namespace Piratas.Servidor.Servico.WebSocket
{
    using Protocolo;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Servidor;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    // TODO: Responsável por obter mensagens de criação, entrada e saída de sala.
    public class SalaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemPartidaCliente mensagemCliente = Parser.Deserializar<MensagemPartidaCliente>(e.Data);
            }
            catch (BaseParserException parserException)
            {
                var mensagem = new MensagemPartidaServidor(parserException.Id, parserException.Message);
                var mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

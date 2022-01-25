namespace Piratas.Servidor.Servico.WebSocket
{
    using WebSocketSharp;
    using WebSocketSharp.Server;

    internal class PartidaController : WebSocketBehavior
    {

        protected override void OnMessage(MessageEventArgs e) => Send(e.Data);
    }
}

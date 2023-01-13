namespace Piratas.Cliente.Servicos
{
    using WebSocketSharp;

    public static class WebSocketServico
    {
        private static WebSocket _webSocket;

        public static void Inicializar()
        {
            string endereco = "ws://localhost";

            _webSocket = new WebSocket(endereco);

            _webSocket.OnMessage += AoReceberMensagem;

            void AoReceberMensagem(object _, MessageEventArgs messageEventArgs)
            {
                string data = messageEventArgs.Data;

                LogServico.Info(data);
            }
        }
    }
}

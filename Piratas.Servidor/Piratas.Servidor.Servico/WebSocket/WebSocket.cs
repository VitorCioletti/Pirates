namespace Piratas.Servidor.Servico.WebSocket
{
    using WebSocketSharp.Server;

    public class WebSocket
    {
        private int _porta;

        private WebSocketServer _conexao;

        public WebSocket(string ip, int porta)
        {
            _porta = porta;
 
            _conexao = new WebSocketServer($"ws://{ip}:{porta}");

            _conexao.AddWebSocketService<PartidaController>("/partida");
        }

        public void Conectar()
        {
            _conexao.Start();
        }

        public void Desconectar()
        {
            _conexao.Stop();
        }
    }
}
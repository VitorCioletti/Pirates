namespace Piratas.Servidor.Servico.WebSocket
{
    using WebSocketSharp.Server;

    public class WebSocket
    {
        private string _endereco;

        private string _porta;

        private WebSocketServer _conexao;

        public WebSocket(string endereco, string porta)
        {
            _endereco = endereco;
            _porta = porta;
 
            _conexao = new WebSocketServer($"ws://{endereco}:{porta}");

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
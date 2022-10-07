namespace Piratas.Servidor.Servico.WebSocket
{
    using WebSocketSharp.Server;

    public class WebSocket
    {
        private readonly string _endereco;

        private readonly string _porta;

        private readonly WebSocketServer _conexao;

        public WebSocket(string endereco, string porta)
        {
            _endereco = endereco;
            _porta = porta;

            _conexao = new WebSocketServer($"ws://{_endereco}:{_porta}");

            _conexao.AddWebSocketService<PartidaController>("/partida");
            _conexao.AddWebSocketService<SalaController>("/sala");
        }

        public void Conectar() => _conexao.Start();

        public void Desconectar() => _conexao.Stop();
    }
}

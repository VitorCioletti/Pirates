namespace Piratas.Servidor.Servico.WebSocket
{
    using Microsoft.Extensions.Configuration;
    using WebSocketSharp.Server;
    using Configuracao = Configuracao.Configuracao;
    using Log = Log.Log;

    public static class WebSocket
    {
        private static string _endereco;

        private static string _porta;

        private static WebSocketServer _conexao;

        public static void Inicializar()
        {
            Log.Info("Servidor inicializado.");

            IConfigurationSection configuracaoWebSocket = Configuracao.Dados.GetSection("WebSocket");

            string endereco = configuracaoWebSocket.GetSection("Endereco").Value;
            string porta = configuracaoWebSocket.GetSection("Porta").Value;

            _endereco = endereco;
            _porta = porta;

            _conexao = new WebSocketServer($"ws://{_endereco}:{_porta}");

            _conexao.AddWebSocketService<PartidaController>("/partida");
            _conexao.AddWebSocketService<SalaController>("/sala");

            Conectar();

            Log.Info($"Escutando no endereço: \"{endereco}:{porta}\".");
        }

        public static void Conectar() => _conexao.Start();

        public static void Desconectar() => _conexao.Stop();
    }
}

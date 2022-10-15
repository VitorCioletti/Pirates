namespace Piratas.Servidor.Servico.WebSocket
{
    using Configuracao;
    using Controllers;
    using Log;
    using Microsoft.Extensions.Configuration;
    using WebSocketSharp.Server;

    public static class WebSocketServico
    {
        private static string _endereco;

        private static string _porta;

        private static WebSocketServer _conexao;

        public static void Inicializar()
        {
            LogServico.Info("Servidor inicializado.");

            IConfigurationSection configuracaoWebSocket = ConfiguracaoServico.Dados.GetSection("WebSocketServico");

            string endereco = configuracaoWebSocket.GetSection("Endereco").Value;
            string porta = configuracaoWebSocket.GetSection("Porta").Value;

            _endereco = endereco;
            _porta = porta;

            _conexao = new WebSocketServer($"ws://{_endereco}:{_porta}");

            _conexao.AddWebSocketService<PartidaController>("/partida");
            _conexao.AddWebSocketService<SalaController>("/sala");

            Conectar();

            LogServico.Info($"Escutando no endereço: \"{endereco}:{porta}\".");
        }

        public static void Conectar() => _conexao.Start();

        public static void Desconectar() => _conexao.Stop();
    }
}

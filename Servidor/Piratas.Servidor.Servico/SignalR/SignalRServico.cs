namespace Piratas.Servidor.Servico.SignalR
{
    using System.Threading.Tasks;
    using Configuracao;
    using Hubs;
    using Log;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SignalRServico
    {
        private static WebApplication _webApplication;

        public static void Inicializar()
        {
            LogServico.Info("Servidor inicializado.");

            IConfigurationSection configuracaoWebSocket = ConfiguracaoServico.Dados.GetSection("WebSocket");

            string endereco = configuracaoWebSocket.GetSection("Endereco").Value;
            string porta = configuracaoWebSocket.GetSection("Porta").Value;

            WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();

            webApplicationBuilder.Services.AddSignalR();

            _webApplication = webApplicationBuilder.Build();

            _webApplication.MapHub<PartidaHub>("/partida");
            _webApplication.MapHub<SalaHub>("/hub");

            LogServico.Info($"Escutando no endereço: \"{endereco}:{porta}\".");
        }

        public static async Task ConectarAsync() => await _webApplication.StartAsync();

        public static async Task Desconectar() => await _webApplication.StopAsync();
    }
}

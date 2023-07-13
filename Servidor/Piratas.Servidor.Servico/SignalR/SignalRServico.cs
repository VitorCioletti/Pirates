namespace Piratas.Servidor.Servico.SignalR;

using System.Threading.Tasks;
using Configuracao;
using Filters;
using Hubs;
using Log;
using MessagePack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

public static class SignalRServico
{
    private static WebApplication _webApplication;

    public static void Inicializar()
    {
        LogServico.Logger.Information("Servidor inicializado.");

        IConfigurationSection configuracaoWebSocket = ConfiguracaoServico.Dados.GetSection("WebSocket");

        string endereco = configuracaoWebSocket.GetSection("Endereco").Value;
        string porta = configuracaoWebSocket.GetSection("Porta").Value;

        WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();

        webApplicationBuilder.Host.UseSerilog(LogServico.Logger);

        ISignalRServerBuilder signalRServerBuilder = webApplicationBuilder.Services.AddSignalR(_adicionarFiltros);
        signalRServerBuilder.AddMessagePackProtocol(_configurarMessagePack);

        _webApplication = webApplicationBuilder.Build();

        _webApplication.MapHub<PartidaHub>("/partida");
        _webApplication.MapHub<SalaHub>("/sala");

        LogServico.Logger.Information($"Escutando no endereço: \"{endereco}:{porta}\".");
    }

    public static async Task ConectarAsync() => await _webApplication.StartAsync();

    public static async Task DesconectarAsync() => await _webApplication.StopAsync();

    private static void _adicionarFiltros(HubOptions hubOptions) => hubOptions.AddFilter<ExcecaoFilter>();

    private static void _configurarMessagePack(MessagePackHubProtocolOptions messagePackHubProtocolOptions)
    {
        messagePackHubProtocolOptions.SerializerOptions =
            MessagePackSerializerOptions.Standard.WithSecurity(MessagePackSecurity.UntrustedData);
    }
}

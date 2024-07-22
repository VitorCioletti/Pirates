namespace Piratas.Servidor.Servico.SignalR;

using System.Threading.Tasks;
using Configuracao;
using Filters;
using Hubs;
using Log;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

public static class SignalRService
{
    private static WebApplication _webApplication;

    public static void ConfigureSignalR()
    {
        LogService.Logger.Information("Server initialized");

        WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder();

        webApplicationBuilder.Host.UseSerilog(LogService.Logger);
        webApplicationBuilder.Configuration.AddConfiguration(ConfigurationService.Data, false);

        ISignalRServerBuilder signalRServerBuilder = webApplicationBuilder.Services.AddSignalR(_addFilters);
        signalRServerBuilder.AddMessagePackProtocol(_configureMessagePack);

        _webApplication = webApplicationBuilder.Build();

        _webApplication.MapHub<MatchHub>("/match");
        _webApplication.MapHub<RoomHub>("/room");
    }

    public static async Task ConnectAsync() => await _webApplication.StartAsync();

    public static async Task DisconnectAsync() => await _webApplication.StopAsync();

    private static void _addFilters(HubOptions hubOptions) => hubOptions.AddFilter<ExceptionFilter>();

    private static void _configureMessagePack(MessagePackHubProtocolOptions messagePackHubProtocolOptions)
    {
        messagePackHubProtocolOptions.SerializerOptions = MessagePackSerializerOptions.Standard
            .WithSecurity(MessagePackSecurity.UntrustedData)
            .WithResolver(ContractlessStandardResolver.Instance);

    }
}

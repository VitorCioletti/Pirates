namespace Piratas.Servidor.Servico.Inicializacao
{
    using Configuracao;
    using Log;
    using Partida;
    using SignalR;

    public static class InitializationService
    {
        public static void Initialize()
        {
            ConfigurationService.GetConfigurationFileData();
            LogService.ConfigureLogger();
            MatchService.ConfigureCardGenerator();
            SignalRService.ConfigureSignalR();
        }
    }
}

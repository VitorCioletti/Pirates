namespace Pirates.Server.Service.Initialization
{
    using Configuration;
    using Log;
    using Match;
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

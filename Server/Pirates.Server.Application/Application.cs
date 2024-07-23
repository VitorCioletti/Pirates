namespace Pirates.Server.Application
{
    using System;
    using System.Threading.Tasks;
    using Service.Initialization;
    using Service.SignalR;

    public static class Application
    {
        public static async Task Main(string[] _)
        {
            InitializationService.Initialize();

            await SignalRService.ConnectAsync();

            Console.Read();

            await SignalRService.DisconnectAsync();
        }
    }
}

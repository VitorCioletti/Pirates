namespace Piratas.Servidor.Aplicacao
{
    using System;
    using System.Threading.Tasks;
    using Servico.Inicializacao;
    using Servico.SignalR;

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

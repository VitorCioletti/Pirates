namespace Piratas.Servidor.Servico.Log
{
    using System;
    using Configuracao;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public static class LogService
    {
        public static ILogger Logger { get; private set; }

        public static void ConfigureLogger()
        {
            Logger = _criarLogger(ConfigurationService.Data);

            _configuraExcecaoNaoTratada();
        }

        private static ILogger _criarLogger(IConfiguration configuracao) =>
            new LoggerConfiguration().ReadFrom.Configuration(configuracao).CreateLogger();

        private static void _configuraExcecaoNaoTratada()
        {
            AppDomain.CurrentDomain.UnhandledException += ExcecaoNaoTratada;

            void ExcecaoNaoTratada(object _, UnhandledExceptionEventArgs args)
            {
                Logger.Error($"Ocorreu um não tratado:\n\"{args.ExceptionObject}\".");
                Logger.Information("Servidor finalizado com erro.");

                Environment.Exit(1);
            }
        }
    }
}

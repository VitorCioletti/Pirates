namespace Pirates.Server.Service.Log
{
    using System;
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Core;

    public static class LogService
    {
        public static ILogger Logger { get; private set; }

        public static void ConfigureLogger()
        {
            Logger = _createLogger(ConfigurationService.Data);

            _configureUnhandledExceptionHandling();
        }

        private static Logger _createLogger(IConfiguration configuration) =>
            new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        private static void _configureUnhandledExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            void UnhandledExceptionHandler(object _, UnhandledExceptionEventArgs args)
            {
                Logger.Error($"An unhandled exception occurred:\n\"{args.ExceptionObject}\".");
                Logger.Information("Server terminated with error.");

                Environment.Exit(1);
            }
        }
    }
}

namespace Piratas.Servidor.Servico.Log
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;

    public class Log
    {
        public ILogger Logger { get; private set; }

        public Log(IConfiguration configuracao)
        {
            Logger = _obterLogger(configuracao);

            _configuraExcecaoNaoTratada();
        }

        private ILogger _obterLogger(IConfiguration configuracao) =>
           new LoggerConfiguration().ReadFrom.Configuration(configuracao).CreateLogger();

        private void _configuraExcecaoNaoTratada()
        {
            void excecaoNaoTratada(object _, UnhandledExceptionEventArgs args)
            {
                Logger.Error($"Ocorreu um não tratado:\n\"{args.ExceptionObject}\".");
                Logger.Information("Servidor finalizado com erro.");

                Environment.Exit(1);
            }

            AppDomain.CurrentDomain.UnhandledException += excecaoNaoTratada;
        }
    }
}

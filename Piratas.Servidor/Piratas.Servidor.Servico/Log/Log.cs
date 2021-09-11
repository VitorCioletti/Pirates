namespace Piratas.Servidor.Servico.Log
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;

    public class Log
    {
        public ILogger Logger;

        public Log(IConfiguration configuracao)
        {
            Logger =  _obterLogger(configuracao);

            _configuraExcecaoNaoTratada();
        }

        private ILogger _obterLogger(IConfiguration configuracao) =>
           new LoggerConfiguration().ReadFrom.Configuration(configuracao).CreateLogger();

        private void _configuraExcecaoNaoTratada()
        {
            AppDomain.CurrentDomain.UnhandledException += 
                (_, e) => 
                {
                    Logger.Error($"Ocorreu um não tratado:\n\"{e.ExceptionObject}\".");
                    Logger.Information("Servidor finalizado com erro.");

                    Environment.Exit(1);
                };
        }
    }
}

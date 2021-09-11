namespace Piratas.Servidor.Servico.Log
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;

    public class Log
    {
        public ILogger Loga;

        public void Configura(IConfiguration configuracao)
        {
            Loga = new LoggerConfiguration().ReadFrom.Configuration(configuracao).CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += 
                (_, e) => 
                {
                    Loga.Error($"Ocorreu um não tratado:\n\"{e.ExceptionObject}\".");
                    Loga.Information("Servidor finalizado com erro.");

                    Environment.Exit(1);
                };
        }
    }
}

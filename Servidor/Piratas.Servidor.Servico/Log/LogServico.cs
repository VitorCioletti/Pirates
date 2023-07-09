namespace Piratas.Servidor.Servico.Log
{
    using System;
    using Configuracao;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public static class LogServico
    {
        public static ILogger Logger { get; set; }

        public static void Inicializar()
        {
            Logger = _criarLogger(ConfiguracaoServico.Dados);

            _configuraExcecaoNaoTratada();
        }

        public static void Info(string mensagem) => Logger.Information(mensagem);

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

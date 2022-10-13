namespace Piratas.Servidor.Servico.Log
{
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;
    using Configuracao = Configuracao.Configuracao;

    // TODO: Melhorar chamada dessa classe pois está Log.Log.
    public static class Log
    {
        private static ILogger _logger { get; set; }

        public static void Inicializar()
        {
            _logger = _obterLogger(Configuracao.Dados);

            _configuraExcecaoNaoTratada();
        }

        public static void Info(string mensagem) => _logger.Information(mensagem);

        private static ILogger _obterLogger(IConfiguration configuracao) =>
            new LoggerConfiguration().ReadFrom.Configuration(configuracao).CreateLogger();

        private static void _configuraExcecaoNaoTratada()
        {
            AppDomain.CurrentDomain.UnhandledException += ExcecaoNaoTratada;

            void ExcecaoNaoTratada(object _, UnhandledExceptionEventArgs args)
            {
                _logger.Error($"Ocorreu um não tratado:\n\"{args.ExceptionObject}\".");
                _logger.Information("Servidor finalizado com erro.");

                Environment.Exit(1);
            }
        }
    }
}

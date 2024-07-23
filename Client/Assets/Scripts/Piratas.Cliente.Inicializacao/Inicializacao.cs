namespace Piratas.Cliente.Bootstrapper
{
    using System;
    using DependencyInjection;
    using DependencyInjection.Setup;
    using Servico;

    public static class Inicializacao
    {
        public static void Inicializar()
        {
            _configurarInjecaoDependencia();

            _inicializarServicos();
        }

        private static void _inicializarServicos()
        {
            SignalRServico.ConfigurarConexao();
        }

        private static void _configurarInjecaoDependencia()
        {
            IServiceProvider serviceProvider = Configuracao.AplicarInjecaoDependencia();

            Resolvedor.SetServiceProvider(serviceProvider);
        }
    }
}

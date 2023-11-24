namespace Piratas.Cliente.Bootstrapper
{
    using System;
    using DependencyInjection;
    using DependencyInjection.Setup;

    public static class Inicializacao
    {
        public static void Boot()
        {
            _configurarInjecaoDependencia();
        }

        private static void _configurarInjecaoDependencia()
        {
            IServiceProvider serviceProvider = Configuracao.AplicarInjecaoDependencia();

            Resolvedor.SetServiceProvider(serviceProvider);
        }
    }
}

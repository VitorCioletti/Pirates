namespace Piratas.Cliente.DependencyInjection.Setup
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class Configuracao
    {
        public static IServiceProvider AplicarInjecaoDependencia()
        {
            var serviceCollection = new ServiceCollection();

            return serviceCollection.BuildServiceProvider();
        }
    }
}

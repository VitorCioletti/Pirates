namespace Piratas.Cliente.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class Resolvedor
    {
        private static IServiceProvider _serviceProvider;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}

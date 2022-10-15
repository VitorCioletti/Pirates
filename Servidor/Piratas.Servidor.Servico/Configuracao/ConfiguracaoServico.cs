namespace Piratas.Servidor.Servico.Configuracao
{
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    public static class ConfiguracaoServico
    {
        public static IConfigurationRoot Dados { get; private set; }

        public static void Inicializar()
        {
            Dados = _obterDados();
        }

        private static IConfigurationRoot _obterDados()
        {
            var caminhoBinario = Assembly.GetExecutingAssembly().Location;
            var pastaBinario = Path.GetDirectoryName(caminhoBinario);

            return new ConfigurationBuilder()
                .SetBasePath(pastaBinario)
                .AddJsonFile($"configuracao.json")
                .Build();
        }
    }
}

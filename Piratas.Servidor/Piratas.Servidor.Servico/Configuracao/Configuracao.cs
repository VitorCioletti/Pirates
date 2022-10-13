namespace Piratas.Servidor.Servico.Configuracao
{
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    // TODO: Melhorar chamada dessa classe pois está Configuracao.Configuracao.
    public static class Configuracao
    {
        public static IConfigurationRoot Dados { get; set; }

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

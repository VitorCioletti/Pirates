namespace Piratas.Servidor.Servico.Configuracao
{
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Reflection;

    public class Configuracao
    {
        public IConfigurationRoot Dados { get; set; }

        public Configuracao() => Dados = _obterDados();

        private IConfigurationRoot _obterDados()
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
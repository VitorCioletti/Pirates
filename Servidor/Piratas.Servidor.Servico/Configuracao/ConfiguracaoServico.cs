namespace Piratas.Servidor.Servico.Configuracao
{
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    public static class ConfiguracaoServico
    {
        public static IConfigurationRoot Dados { get; private set; }

        public static void ObterDadosArquivoConfiguracao()
        {
            Dados = _obterDados();
        }

        private static IConfigurationRoot _obterDados()
        {
            string caminhoBinario = Assembly.GetExecutingAssembly().Location;
            string pastaBinario = Path.GetDirectoryName(caminhoBinario);

            return new ConfigurationBuilder()
                .SetBasePath(pastaBinario)
                .AddJsonFile("Configuracao/Arquivos/configuracao.json")
                .Build();
        }
    }
}

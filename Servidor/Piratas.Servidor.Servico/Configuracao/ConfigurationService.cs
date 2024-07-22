namespace Piratas.Servidor.Servico.Configuracao
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationService
    {
        public static IConfigurationRoot Data { get; private set; }

        public static void GetConfigurationFileData()
        {
            Data = _getData();
        }

        private static IConfigurationRoot _getData()
        {
            string binaryPath = Assembly.GetExecutingAssembly().Location;
            string binaryFolder = Path.GetDirectoryName(binaryPath);

            if (binaryFolder is null)
                throw new InvalidOperationException("Folder not found.");

            return new ConfigurationBuilder()
                .SetBasePath(binaryFolder)
                .AddJsonFile("Configuration/Folders/configuration.json")
                .Build();
        }
    }
}

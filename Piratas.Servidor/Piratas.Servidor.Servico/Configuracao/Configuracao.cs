namespace Piratas.Servidor.Servidor.Configuracao
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Configuracao
    {
        public IConfigurationRoot Dados { get; set; }

        public void Carrega()
        {
            Dados = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"Arquivos/configuracao.json")
                .Build();
        }
    }
}
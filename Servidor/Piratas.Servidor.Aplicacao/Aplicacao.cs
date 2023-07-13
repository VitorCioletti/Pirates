namespace Piratas.Servidor.Aplicacao
{
    using System;
    using System.Threading.Tasks;
    using Servico.Inicializacao;
    using Servico.SignalR;

    public static class Aplicacao
    {
        public static async Task Main(string[] _)
        {
            InicializacaoServico.Inicializar();

            await SignalRServico.ConectarAsync();

            Console.Read();

            await SignalRServico.DesconectarAsync();
        }
    }
}

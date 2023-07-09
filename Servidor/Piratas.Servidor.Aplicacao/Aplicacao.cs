namespace Piratas.Servidor.Aplicacao
{
    using System;
    using System.Threading.Tasks;
    using Servico.Inicializacao;
    using Servico.SignalR;

    public static class Aplicacao
    {
        public static async Task Main()
        {
            InicializacaoServico.Inicializar();

            await SignalRServico.ConectarAsync();

            Console.ReadKey();

            await SignalRServico.DesconectarAsync();
        }
    }
}

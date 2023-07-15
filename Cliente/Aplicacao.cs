namespace Piratas.Cliente
{
    using System;
    using System.Threading.Tasks;
    using MaquinaEstados.Estados;
    using Servicos;

    public static class Aplicacao
    {
        public static async Task Main()
        {
            try
            {
                InicializacaoServico.Inicializar();

                await SignalRServico.ConectarAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Erro de inicialização não tratado.");
                Console.WriteLine(exception);

                return;
            }

            var maquinaEstados = new MaquinaEstados.MaquinaEstados();

            SignalRServico.RegistrarSalaOuvinte(maquinaEstados);

            maquinaEstados.Adicionar(new MenuEstado(maquinaEstados));

            while (true)
            {
                string texto = Console.ReadLine();

                BaseEstado estadoAtual = maquinaEstados.ObterAtual();

                if (estadoAtual == null)
                    break;

                estadoAtual.AoReceberTexto(texto);
            }

            SignalRServico.RemoverSalaOuvinte(maquinaEstados);

            await SignalRServico.DesconectarAsync();
        }
    }
}

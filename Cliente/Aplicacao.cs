namespace Piratas.Cliente
{
    using System;
    using MaquinaEstados.Estados;
    using Servicos;

    public static class Aplicacao
    {
        public static void Main()
        {
            try
            {
                InicializacaoServico.Inicializar();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Erro de inicialização não tratado.");
                Console.WriteLine(exception);

                return;
            }


            var maquinaEstados = new MaquinaEstados.MaquinaEstados();

            maquinaEstados.Adicionar(new MenuEstado(maquinaEstados));

            while (true)
            {
                string texto = Console.ReadLine();

                BaseEstado estadoAtual = maquinaEstados.ObterAtual();

                if (estadoAtual == null)
                    break;

                estadoAtual.AoReceberTexto(texto);
            }
        }
    }
}

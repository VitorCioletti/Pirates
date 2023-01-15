namespace Piratas.Cliente
{
    using System;
    using MaquinaEstados.Estados;
    using Servicos;

    public static class Aplicacao
    {
        public static void Main()
        {
            InicializacaoServico.Inicializar();

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

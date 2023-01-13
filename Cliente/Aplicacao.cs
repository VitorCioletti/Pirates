namespace Piratas.Cliente
{
    using System;
    using Servicos;

    public static class Aplicacao
    {
        public static void Main(string[] args)
        {
            InicializacaoServico.Inicializar();

            Console.ReadLine();
        }
    }
}

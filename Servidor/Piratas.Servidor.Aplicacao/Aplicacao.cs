namespace Piratas.Servidor.Aplicacao
{
    using System;
    using Servico.Inicializacao;

    public class Aplicacao
    {
        public static void Main(string[] _)
        {
            InicializacaoServico.Inicializar();

            Console.ReadKey();
        }
    }
}

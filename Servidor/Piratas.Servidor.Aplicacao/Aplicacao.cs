namespace Piratas.Servidor.Aplicacao
{
    using System;
    using Servico.Inicializacao;

    public class Aplicacao
    {
        public static void Main()
        {
            InicializacaoServico.Inicializar();

            Console.Read();
        }
    }
}

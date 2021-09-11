namespace Piratas.Servidor.Inicializador
{
    using System;
    using Servico.WebSocket;

    class Program
    {
        static void Main(string[] args)
        {
            var porta = 8182;

            Console.WriteLine("Inicializado servidor.");

            var webSocket = new WebSocket(porta);

            webSocket.Conectar();
            Console.WriteLine($"Escutando na porta: \"{porta}\".");

            Console.ReadLine();
        }
    }
}

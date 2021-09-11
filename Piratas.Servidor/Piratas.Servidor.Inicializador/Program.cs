namespace Piratas.Servidor.Inicializador
{
    using System;
    using Servico.WebSocket;

    class Program
    {
        static void Main(string[] args)
        {
            var ip = "0.0.0.0";
            var porta = 8182;

            Console.WriteLine("Inicializado servidor.");

            var webSocket = new WebSocket(ip, porta);

            webSocket.Conectar();
            Console.WriteLine($"Escutando na porta: \"{porta}\".");

            Console.ReadLine();
        }
    }
}

namespace Piratas.Servidor.Aplicacao
{
    using System;
    using Servico.WebSocket;

    class Aplicacao
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

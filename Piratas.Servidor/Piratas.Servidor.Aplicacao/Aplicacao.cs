namespace Piratas.Servidor.Aplicacao
{
    using Servico.Configuracao;
    using Servico.Log;
    using Servico.WebSocket;
    using System;

    public class Aplicacao
    {
        public static void Main(string[] _)
        {
            var configuracao = new Configuracao();

            var log = new Log(configuracao.Dados);

            log.Logger.Debug("Servidor inicializado.");

            var configuracaoWebSocket = configuracao.Dados.GetSection("WebSocket");

            var endereco = configuracaoWebSocket.GetSection("Endereco").Value;
            var porta = configuracaoWebSocket.GetSection("Porta").Value;

            var webSocket = new WebSocket(endereco, porta);

            webSocket.Conectar();
            log.Logger.Debug($"Escutando no endereço: \"{endereco}:{porta}\".");

            Console.ReadKey();
        }
    }
}

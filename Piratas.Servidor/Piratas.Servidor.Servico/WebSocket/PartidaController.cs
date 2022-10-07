namespace Piratas.Servidor.Servico.WebSocket
{
    using System.Collections.Generic;
    using Partida;
    using Protocolo;
    using Protocolo.Cliente;
    using Protocolo.Servidor;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class PartidaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemCliente mensagemCliente = Parser.Deserializa<MensagemCliente>(e.Data);

                List<MensagemServidor> mensagensServidor =
                    GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemCliente);

                foreach (MensagemServidor mensagemServidor in mensagensServidor)
                {
                    string mensagemServidorDeserializada = Parser.Serializa(mensagemServidor);

                    // TODO: Enviar resposta para as diferentes sessões dos jogadores. Pesquisar propriedade Sessions.
                    Send(mensagemServidorDeserializada);
                }
            }
            catch (ParserException parserException)
            {
                var mensagem = new MensagemServidor(parserException.Id);
                var mensagemSerializada = Parser.Serializa(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

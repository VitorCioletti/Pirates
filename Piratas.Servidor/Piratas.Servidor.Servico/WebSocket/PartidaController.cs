namespace Piratas.Servidor.Servico.WebSocket
{
    using System.Collections.Generic;
    using Partida;
    using Protocolo;
    using Protocolo.Cliente.Partida;
    using Protocolo.Servidor.Partida;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class PartidaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemPartidaCliente mensagemCliente = Parser.Deserializa<MensagemPartidaCliente>(e.Data);

                List<MensagemPartidaServidor> mensagensServidor =
                    GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemCliente);

                foreach (MensagemPartidaServidor mensagemServidor in mensagensServidor)
                {
                    string mensagemServidorDeserializada = Parser.Serializa(mensagemServidor);

                    // TODO: Enviar resposta para as diferentes sessões dos jogadores. Pesquisar propriedade Sessions.
                    Send(mensagemServidorDeserializada);
                }
            }
            catch (ParserException parserException)
            {
                var mensagem = new MensagemPartidaServidor(parserException.Id);
                var mensagemSerializada = Parser.Serializa(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

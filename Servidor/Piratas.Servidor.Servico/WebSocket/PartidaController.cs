namespace Piratas.Servidor.Servico.WebSocket
{
    using System;
    using System.Collections.Generic;
    using Partida;
    using Protocolo;
    using Protocolo.Partida.Cliente;
    using Protocolo.Partida.Servidor;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class PartidaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemPartidaCliente mensagemCliente = Parser.Deserializar<MensagemPartidaCliente>(e.Data);

                List<MensagemPartidaServidor> mensagensServidor =
                    GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemCliente);

                foreach (MensagemPartidaServidor mensagemServidor in mensagensServidor)
                {
                    string mensagemServidorDeserializada = Parser.Serializar(mensagemServidor);

                    // TODO: Enviar resposta para as diferentes sessões dos jogadores. Pesquisar propriedade Sessions.
                    Send(mensagemServidorDeserializada);
                }
            }
            // TODO: Tentar fazer captura de exceções em controller em outro lugar pois será necessário repetir em
            // todos
            catch (BaseParserException parserException)
            {
                var mensagem = new MensagemPartidaServidor(parserException.Id, parserException.Message);
                var mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
            // TODO: Tentar fazer captura de exceções em controller em outro lugar pois será necessário repetir em
            // todos
            catch (Exception exception)
            {
                var mensagem = new MensagemPartidaServidor("erro-desconhecido", exception.Message);
                var mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

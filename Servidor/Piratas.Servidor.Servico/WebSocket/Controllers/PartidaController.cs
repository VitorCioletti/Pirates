namespace Piratas.Servidor.Servico.WebSocket.Controllers
{
    using System;
    using System.Collections.Generic;
    using Excecoes.Partida;
    using Partida;
    using Protocolo;
    using Protocolo.Excecoes;
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
                var mensagemCliente = Parser.Deserializar<MensagemPartidaCliente>(e.Data);

                List<MensagemPartidaServidor> mensagensServidor =
                    GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemCliente);

                foreach (MensagemPartidaServidor mensagemServidor in mensagensServidor)
                {
                    string mensagemServidorDeserializada = Parser.Serializar(mensagemServidor);

                    // TODO: Enviar resposta para as diferentes sessões dos jogadores. Pesquisar propriedade Sessions.
                    Send(mensagemServidorDeserializada);
                }
            }
            catch (BasePartidaExcecao partidaException)
            {
                var mensagem = new MensagemPartidaServidor(partidaException.Id, partidaException.Message);
                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
            // TODO: Tentar fazer captura de exceções em controller em outro lugar pois será necessário repetir em
            // todos
            catch (BaseParserExcecao parserException)
            {
                var mensagem = new MensagemPartidaServidor(parserException.Id, parserException.Message);
                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
            // TODO: Tentar fazer captura de exceções em controller em outro lugar pois será necessário repetir em
            // todos
            catch (Exception exception)
            {
                var mensagem = new MensagemPartidaServidor("erro-desconhecido", exception.Message);
                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

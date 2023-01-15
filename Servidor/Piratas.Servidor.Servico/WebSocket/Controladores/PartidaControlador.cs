namespace Piratas.Servidor.Servico.WebSocket.Controladores
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

    public class PartidaControlador : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Guid idMensagemCliente = Guid.Empty;

            try
            {
                var mensagemCliente = Parser.Deserializar<MensagemPartidaCliente>(e.Data);
                idMensagemCliente = mensagemCliente.Id;

                List<MensagemPartidaServidor> mensagensServidor =
                    GerenciadorPartidaServico.ProcessarMensagemCliente(mensagemCliente);

                foreach (MensagemPartidaServidor mensagemServidor in mensagensServidor)
                {
                    string mensagemServidorDeserializada = Parser.Serializar(mensagemServidor);

                    Send(mensagemServidorDeserializada);
                }
            }
            catch (BasePartidaExcecao partidaException)
            {
                var mensagem = new MensagemPartidaServidor(
                    idMensagemCliente,
                    partidaException.Id,
                    partidaException.Message);

                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
            catch (BaseParserExcecao parserException)
            {
                var mensagem = new MensagemPartidaServidor(
                    idMensagemCliente,
                    parserException.Id,
                    parserException.Message);

                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
            catch (Exception exception)
            {
                var mensagem = new MensagemPartidaServidor(idMensagemCliente, "erro-desconhecido", exception.Message);
                string mensagemSerializada = Parser.Serializar(mensagem);

                Send(mensagemSerializada);
            }
        }
    }
}

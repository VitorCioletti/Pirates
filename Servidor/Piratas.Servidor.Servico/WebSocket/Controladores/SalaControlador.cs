namespace Piratas.Servidor.Servico.WebSocket.Controladores
{
    using System;
    using System.Collections.Generic;
    using Excecoes.Sala;
    using Protocolo;
    using Protocolo.Excecoes;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;
    using Sala;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class SalaControlador : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Guid idMensagemSalaCliente = Guid.Empty;

            try
            {
                var mensagemSalaCliente = Parser.Deserializar<MensagemSalaCliente>(e.Data);
                idMensagemSalaCliente = mensagemSalaCliente.Id;

                List<MensagemSalaServidor> mensagensSalaServidor =
                    SalaServico.ProcessarMensagemCliente(mensagemSalaCliente);

                foreach (MensagemSalaServidor mensagemSalaServidor in mensagensSalaServidor)
                {
                    string mensagemSalaServidorSerializada = Parser.Serializar(mensagemSalaServidor);

                    Send(mensagemSalaServidorSerializada);
                }
            }
            catch (BaseSalaExcecao baseSalaException)
            {
                _enviaMensagemErro(idMensagemSalaCliente, baseSalaException.Id, baseSalaException.Message);
            }
            catch (BaseParserExcecao parserException)
            {
                _enviaMensagemErro(idMensagemSalaCliente, parserException.Id, parserException.Message);
            }
        }

        private void _enviaMensagemErro(Guid idMensagemCliente, string idErro, string descricaoErro)
        {
            var mensagem = new MensagemSalaServidor(idMensagemCliente, idErro, descricaoErro);
            var mensagemSerializada = Parser.Serializar(mensagem);

            Send(mensagemSerializada);
        }
    }
}

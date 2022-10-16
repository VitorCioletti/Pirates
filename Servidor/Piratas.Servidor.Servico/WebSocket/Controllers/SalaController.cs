namespace Piratas.Servidor.Servico.WebSocket.Controllers
{
    using System.Collections.Generic;
    using Excecoes.Sala;
    using Protocolo;
    using Protocolo.Excecoes;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;
    using Sala;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class SalaController : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                MensagemSalaCliente mensagemSalaCliente = Parser.Deserializar<MensagemSalaCliente>(e.Data);

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
                _enviaMensagemErro(baseSalaException.Id, baseSalaException.Message);
            }
            catch (BaseParserExcecao parserException)
            {
                _enviaMensagemErro(parserException.Id, parserException.Message);
            }
        }

        private void _enviaMensagemErro(string idErro, string descricaoErro)
        {
            var mensagem = new MensagemSalaServidor(idErro, descricaoErro);
            var mensagemSerializada = Parser.Serializar(mensagem);

            Send(mensagemSerializada);
        }
    }
}

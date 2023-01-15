namespace Piratas.Cliente.Servicos
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Protocolo;
    using WebSocketSharp;

    public static class WebSocketServico
    {
        public static List<BaseMensagem> MensagensRecebidas { get; private set; }

        private static WebSocket _webSocket;

        static WebSocketServico()
        {
            MensagensRecebidas = new List<BaseMensagem>();
        }

        public static void Inicializar()
        {
            string endereco = "ws://0.0.0.0:8182";

            _webSocket = new WebSocket(endereco);

            _webSocket.OnMessage += AoReceberMensagem;

            void AoReceberMensagem(object _, MessageEventArgs messageEventArgs)
            {
                string mensagem = messageEventArgs.Data;

                LogServico.Info(mensagem);

                var mensagemDeserializada = Parser.Deserializar<BaseMensagem>(mensagem);

                MensagensRecebidas.Add(mensagemDeserializada);
            }
        }

        public static BaseMensagem Enviar(BaseMensagem mensagem)
        {
            string mensagemSerializada = Parser.Serializar(mensagem);

            _webSocket.Send(mensagemSerializada);

            while (true)
            {
                List<BaseMensagem> mensagensRecebidas = MensagensRecebidas.ToList();

                foreach (BaseMensagem mensagemRecebida in mensagensRecebidas)
                {
                    if (mensagemRecebida.IdMensagemSolicitante == mensagem.Id)
                    {
                        MensagensRecebidas.Remove(mensagemRecebida);
                        return mensagemRecebida;
                    }
                }

                Thread.Sleep(100);
            }
        }
    }
}

namespace Piratas.Cliente.Servicos
{
    using System;
    using Protocolo.Sala.Cliente;
    using Protocolo.Sala.Servidor;

    public static class SalaServico
    {
        public static MensagemSalaServidor CriarSala()
        {
            var mensagem = new MensagemSalaCliente(Guid.Empty, Guid.Empty, Guid.Empty, TipoOperacaoSala.Criar);

            var respostaServidor = (MensagemSalaServidor)WebSocketServico.EnviarMensagemSala(mensagem);

            return respostaServidor;
        }

        public static MensagemSalaServidor EntrarSala(string idSala)
        {
            Guid guidIdSala = Guid.Parse(idSala);

            var mensagem = new MensagemSalaCliente(Guid.Empty, guidIdSala, Guid.Empty, TipoOperacaoSala.Entrar);

            var respostaServidor = (MensagemSalaServidor)WebSocketServico.EnviarMensagemSala(mensagem);

            return respostaServidor;
        }
    }
}

namespace Piratas.Protocolo.Sala.Servidor
{
    using System;

    public class MensagemSalaServidor : Mensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public string IdJogadorRealizouAcao { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            string idJogadorRealizouAcao,
            Guid idPartida,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            IdPartida = idPartida;
        }

        public MensagemSalaServidor(
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdPartida = Guid.Empty;
            IdSala = Guid.Empty;
            IdJogadorRealizouAcao = String.Empty;
        }
    }
}

namespace Piratas.Protocolo.Sala.Servidor
{
    using System;

    public class MensagemSalaServidor : BaseMensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public string IdJogador { get; private set; }

        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoOperacaoSalaServidor TipoOperacaoSala { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            string idJogador,
            Guid idJogadorRealizouAcao,
            Guid idPartida,
            Guid idMensagemSolicitante,
            TipoOperacaoSalaServidor tipoOperacaoSala,
            string idErro = null,
            string descricaoErro = null) : base(idMensagemSolicitante, idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoOperacaoSala = tipoOperacaoSala;
            IdPartida = idPartida;
            IdJogador = idJogador;
        }

        public MensagemSalaServidor(
            Guid idMensagemSolicitante,
            string idErro = null,
            string descricaoErro = null) : base(idMensagemSolicitante, idErro, descricaoErro)
        {
            IdPartida = Guid.Empty;
            IdJogador = string.Empty;
            IdSala = Guid.Empty;
            IdJogadorRealizouAcao = Guid.Empty;
            TipoOperacaoSala = TipoOperacaoSalaServidor.Desconhecido;
        }
    }
}

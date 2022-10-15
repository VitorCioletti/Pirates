namespace Piratas.Protocolo.Sala.Servidor
{
    using System;

    public class MensagemSalaServidor : BaseMensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public Guid IdJogador { get; private set; }

        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoOperacaoSalaServidor TipoOperacaoSala { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            Guid idJogador,
            Guid idJogadorRealizouAcao,
            Guid idPartida,
            TipoOperacaoSalaServidor tipoOperacaoSala,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoOperacaoSala = tipoOperacaoSala;
            IdPartida = idPartida;
            IdJogador = idJogador;
        }

        public MensagemSalaServidor(string idErro = null, string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdPartida = Guid.Empty;
            IdJogador = Guid.Empty;
            IdSala = Guid.Empty;
            IdJogadorRealizouAcao = Guid.Empty;
            TipoOperacaoSala = TipoOperacaoSalaServidor.Desconhecido;
        }
    }
}

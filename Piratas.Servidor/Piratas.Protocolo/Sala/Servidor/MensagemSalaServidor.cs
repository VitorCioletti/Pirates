namespace Piratas.Protocolo.Sala.Servidor
{
    using System;

    public class MensagemSalaServidor : BaseMensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public Guid IdJogador { get; private set; }

        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoAcaoSalaServidor TipoAcaoSala { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            Guid idJogador,
            Guid idJogadorRealizouAcao,
            Guid idPartida,
            TipoAcaoSalaServidor tipoAcaoSala,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoAcaoSala = tipoAcaoSala;
            IdPartida = idPartida;
            IdJogador = idJogador;
        }

        public MensagemSalaServidor(string idErro = null, string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdPartida = Guid.Empty;
            IdJogador = Guid.Empty;
            IdSala = Guid.Empty;
            IdJogadorRealizouAcao = Guid.Empty;
            TipoAcaoSala = TipoAcaoSalaServidor.Desconhecido;
        }
    }
}

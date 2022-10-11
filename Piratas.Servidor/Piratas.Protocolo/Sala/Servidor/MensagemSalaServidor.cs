namespace Piratas.Protocolo.Sala.Servidor
{
    using System;
    using BaseInternal;

    public class MensagemSalaServidor : BaseMensagem
    {
        public Guid IdSala { get; private set; }

        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoAcaoSalaServidor TipoAcaoSala { get; private set; }

        public MensagemSalaServidor(
            Guid idSala,
            Guid idJogadorRealizouAcao,
            TipoAcaoSalaServidor tipoAcaoSala,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoAcaoSala = tipoAcaoSala;
        }
    }
}

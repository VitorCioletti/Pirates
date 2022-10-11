namespace Piratas.Protocolo.Sala.Servidor
{
    using System;
    using Piratas.Protocolo.BaseInternal;

    public class MensagemSalaServidor : BaseMensagemSala
    {
        public Guid IdSala { get; private set; }

        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoAcaoSalaServidor TipoAcaoSala { get; private set; }

        public MensagemSalaServidor(Guid idSala, Guid idJogadorRealizouAcao, TipoAcaoSalaServidor tipoAcaoSala)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoAcaoSala = tipoAcaoSala;
        }
    }
}

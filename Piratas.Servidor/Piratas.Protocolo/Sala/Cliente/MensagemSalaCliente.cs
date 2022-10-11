namespace Piratas.Protocolo.Sala.Cliente
{
    using System;
    using Piratas.Protocolo.BaseInternal;

    public class MensagemSalaCliente : BaseMensagemSala
    {
        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoAcaoSala TipoAcaoSala { get; private set; }

        public MensagemSalaCliente(Guid idJogadorRealizouAcao, TipoAcaoSala tipoAcaoSala)
        {
            IdJogadorRealizouAcao = idJogadorRealizouAcao;
            TipoAcaoSala = tipoAcaoSala;
        }
    }
}

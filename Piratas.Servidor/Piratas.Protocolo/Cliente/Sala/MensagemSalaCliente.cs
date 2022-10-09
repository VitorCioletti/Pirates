namespace Piratas.Protocolo.Cliente.Sala
{
    using System;

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

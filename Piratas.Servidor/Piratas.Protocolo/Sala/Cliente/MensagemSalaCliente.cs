namespace Piratas.Protocolo.Sala.Cliente
{
    using System;

    public class MensagemSalaCliente : BaseMensagem
    {
        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoAcaoSala TipoAcaoSala { get; private set; }

        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public MensagemSalaCliente(
            Guid idJogadorRealizouAcao,
            Guid idSala,
            Guid idPartida,
            TipoAcaoSala tipoAcaoSala)
        {
            IdSala = idSala;
            IdPartida = idPartida;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;

            TipoAcaoSala = tipoAcaoSala;
        }
    }
}

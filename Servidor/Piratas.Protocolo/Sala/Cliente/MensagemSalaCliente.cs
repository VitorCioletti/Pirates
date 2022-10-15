namespace Piratas.Protocolo.Sala.Cliente
{
    using System;

    public class MensagemSalaCliente : BaseMensagem
    {
        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoOperacaoSala TipoOperacaoSala { get; private set; }

        public Guid IdSala { get; private set; }

        public Guid IdPartida { get; private set; }

        public MensagemSalaCliente(
            Guid idJogadorRealizouAcao,
            Guid idSala,
            Guid idPartida,
            TipoOperacaoSala tipoOperacaoSala)
        {
            IdSala = idSala;
            IdPartida = idPartida;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;

            TipoOperacaoSala = tipoOperacaoSala;
        }
    }
}

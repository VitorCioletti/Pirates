namespace Piratas.Protocolo.Sala.Cliente
{
    using System;

    public class MensagemSalaCliente : BaseMensagem
    {
        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoOperacaoSala TipoOperacaoSala { get; private set; }

        public Guid IdSala { get; private set; }

        public MensagemSalaCliente(
            Guid idJogadorRealizouAcao,
            Guid idSala,
            TipoOperacaoSala tipoOperacaoSala)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;

            TipoOperacaoSala = tipoOperacaoSala;
        }
    }
}

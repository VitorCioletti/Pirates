namespace Piratas.Protocolo.Sala.Cliente
{
    using System;

    public class MensagemSalaCliente : Mensagem
    {
        public Guid IdJogadorRealizouAcao { get; private set; }

        public TipoOperacaoSala TipoOperacaoSala { get; private set; }

        public Guid IdSala { get; private set; }

        public MensagemSalaCliente(
            Guid idJogadorRealizouAcao,
            Guid idSala,
            Guid idMensagemSolicitante,
            TipoOperacaoSala tipoOperacaoSala) : base(idMensagemSolicitante)
        {
            IdSala = idSala;
            IdJogadorRealizouAcao = idJogadorRealizouAcao;

            TipoOperacaoSala = tipoOperacaoSala;
        }
    }
}

namespace Piratas.Protocolo
{
    using System;

    public abstract class BaseMensagemPartida : BaseMensagem
    {
        public Guid IdMesa { get; private set; }

        public string IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        protected BaseMensagemPartida(
            string idJogador,
            Guid idMesa,
            Guid idMensagemSolicitante,
            string idErro = null,
            string descricaoErro = null) : base(idMensagemSolicitante, idErro, descricaoErro)
        {
            IdJogadorRealizador = idJogador;
            IdMesa = idMesa;
            DataHora = DateTime.UtcNow;
        }
    }
}

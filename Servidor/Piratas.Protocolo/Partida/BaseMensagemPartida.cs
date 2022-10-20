namespace Piratas.Protocolo
{
    using System;

    public abstract class BaseMensagemPartida : BaseMensagem
    {
        public Guid IdMesa { get; private set; }

        public Guid IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        protected BaseMensagemPartida(
            Guid idJogador,
            Guid idMesa,
            string idErro = null,
            string descricaoErro = null) : base(idErro, descricaoErro)
        {
            IdJogadorRealizador = idJogador;
            IdMesa = idMesa;
            DataHora = DateTime.UtcNow;
        }
    }
}

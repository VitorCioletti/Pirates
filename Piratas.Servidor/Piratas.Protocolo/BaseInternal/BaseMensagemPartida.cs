namespace Piratas.Protocolo.BaseInternal
{
    using System;

    public abstract class BaseMensagemPartida : BaseMensagemServidor
    {
        public Guid IdMesa { get; private set; }

        public Guid IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        public BaseMensagemPartida(Guid idJogador, Guid idMesa, string idErro = null) : base(idErro)
        {
            IdJogadorRealizador = idJogador;
            IdMesa = idMesa;
            DataHora = DateTime.UtcNow;
        }
    }
}

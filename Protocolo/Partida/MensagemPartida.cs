namespace Piratas.Protocolo
{
    using System;

    public abstract class MensagemPartida : Mensagem
    {
        public Guid IdMesa { get; private set; }

        public string IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        protected MensagemPartida(
            string idJogador,
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

namespace Piratas.Protocolo
{
    using System;

    public abstract class Mensagem
    {
        public Guid Id { get; private set; }

        public Guid IdMesa { get; private set; }

        public Guid IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        public Mensagem(Guid idJogador, Guid idMesa)
        {
            Id = Guid.NewGuid();
            IdJogadorRealizador = idJogador;
            IdMesa = idMesa;
            DataHora = DateTime.UtcNow;
        }
    }
}

namespace Piratas.Protocolo
{
    using System;

    public abstract class Pacote
    {
        public Guid Id { get; private set; }

        public Guid IdJogadorRealizador { get; private set; }

        public DateTime DataHora { get; private set; }

        public Pacote(Guid idJogador)
        {
            Id = Guid.NewGuid();
            IdJogadorRealizador = idJogador;
            DataHora = DateTime.UtcNow;
        }
    }
}

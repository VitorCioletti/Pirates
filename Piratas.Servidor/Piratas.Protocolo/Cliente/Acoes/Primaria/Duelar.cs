namespace Piratas.Protocolo.Cliente
{
    using System;
    using Cartas;

    public class Duelar : Pacote
    {
        public Guid JogadorDesafiado { get; private set; }

        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Guid idJogador, Guid jogadorDesafiado, Duelo cartaIniciadora) : base(idJogador)
        {
            JogadorDesafiado = jogadorDesafiado;
            CartaIniciadora = cartaIniciadora;
        }
    }
}

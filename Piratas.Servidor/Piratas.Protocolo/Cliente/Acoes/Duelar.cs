namespace Piratas.Protocolo.Cliente.Acoes
{
    using System;
    using Cartas;

    public class Duelar : Pacote
    {
        public Guid JogadorDesafiado { get; private set; }

        public Carta CartaIniciadora { get; private set; }

        public Duelar(Guid idJogador, Guid jogadorDesafiado, Carta cartaIniciadora) : base(idJogador)
        {
            JogadorDesafiado = jogadorDesafiado;
            CartaIniciadora = cartaIniciadora;
        }
    }
}

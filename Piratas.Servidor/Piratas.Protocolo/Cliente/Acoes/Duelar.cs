namespace Piratas.Protocolo.Cliente.Acoes
{
    using System;

    public class Duelar : Pacote
    {
        public Guid JogadorDesafiado { get; private set; }

        public string IdCartaIniciadora { get; private set; }

        public Duelar(Guid idJogador, Guid jogadorDesafiado, string idCartaIniciadora) : base(idJogador)
        {
            JogadorDesafiado = jogadorDesafiado;
            IdCartaIniciadora = idCartaIniciadora;
        }
    }
}

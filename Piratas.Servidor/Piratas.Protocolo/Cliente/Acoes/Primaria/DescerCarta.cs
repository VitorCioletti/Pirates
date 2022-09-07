namespace Piratas.Protocolo.Cliente
{
    using System;
    using Piratas.Servidor;
    using Piratas.Servidor.Dominio.Cartas;

    public class DescerCarta : Pacote
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Guid idJogador, Carta carta) : base(idJogador)
        {
            Carta = carta;
        }
    }
}

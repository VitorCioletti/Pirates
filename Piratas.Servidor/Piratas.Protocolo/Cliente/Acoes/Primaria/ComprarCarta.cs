namespace Piratas.Protocolo.Cliente
{
    using System;
    using Piratas.Servidor;

    public class ComprarCarta : Pacote
    {
        public ComprarCarta(Guid idJogador) : base(idJogador)
        {
        }
    }
}

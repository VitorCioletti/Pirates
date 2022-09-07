namespace Piratas.Protocolo.Cliente.Acoes
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

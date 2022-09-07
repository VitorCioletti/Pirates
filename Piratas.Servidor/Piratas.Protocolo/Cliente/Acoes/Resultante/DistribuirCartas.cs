namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;

    public class DistribuirCartas : Pacote
    {
        public Dictionary<Guid, Carta> CartasPorJogador { get; private set; }

        public DistribuirCartas(Guid idJogador, Dictionary<Guid, Carta> cartasPorJogador) : base(idJogador)
        {
            CartasPorJogador = cartasPorJogador;
        }
    }
}

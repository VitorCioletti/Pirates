namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;

    public class RoubarCarta : Pacote
    {
        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Guid idJogador, Carta cartaRoubada) : base(idJogador)
        {
            CartaRoubada = cartaRoubada;
        }
    }
}

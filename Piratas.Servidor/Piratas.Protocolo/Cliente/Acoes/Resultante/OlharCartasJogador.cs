namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;

    public class OlharCartasJogador : Pacote
    {
        public List<Carta> Cartas { get; private set; }

        public OlharCartasJogador(Guid idJogador, List<Carta> cartas) : base(idJogador)
        {
            Cartas = cartas;
        }
    }
}

namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Cartas;

    public class DescerCartasDuelo : Pacote
    {
        public List<Duelo> CartasDueloResposta { get; private set; }

        public DescerCartasDuelo(Guid idJogador, List<Duelo> cartasDueloResposta) : base(idJogador)
        {
            CartasDueloResposta = cartasDueloResposta;
        }
    }
}

namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Cartas;

    public class DescerCartasDueloSurpresa : Pacote
    {
        public List<Duelo> CartasDueloResposta { get; private set; }

        public DescerCartasDueloSurpresa(Guid idJogador, List<Duelo> cartasDueloResposta) : base(idJogador)
        {
            CartasDueloResposta = cartasDueloResposta;
        }
    }
}

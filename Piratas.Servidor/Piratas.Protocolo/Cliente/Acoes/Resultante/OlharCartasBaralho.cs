namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using Protocolo;

    public class OlharCartasBaralho : Pacote
    {
        public bool DevolverNoTopo { get; private set; }

        public List<Carta> CartasOpcoes { get; private set; }

        public OlharCartasBaralho(Guid idJogador, List<Carta> cartasOpcoes, bool devolverNoTopo) : base(idJogador)
        {
            CartasOpcoes = cartasOpcoes;
            DevolverNoTopo = devolverNoTopo;
        }
    }
}

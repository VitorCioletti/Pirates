namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;

    public class DescartarCarta : Pacote
    {
        public Carta Descartada { get; private set; }

        public DescartarCarta(Guid idJogador, Carta descartada) : base(idJogador)
        {
            Descartada = descartada;
        }
    }
}

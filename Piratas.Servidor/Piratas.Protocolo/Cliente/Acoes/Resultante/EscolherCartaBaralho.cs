namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;

    public class EscolherCartaBaralho : Pacote
    {
        public Carta Carta { get; private set; }

        public EscolherCartaBaralho(Guid idJogador, Carta carta) : base(idJogador)
        {
            Carta = carta;
        }
    }
}

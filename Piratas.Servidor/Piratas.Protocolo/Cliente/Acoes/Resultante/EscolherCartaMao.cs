namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;

    public class EscolherCartaMao : Pacote
    {
        public Carta Carta { get; private set; }

        public EscolherCartaMao(Guid idJogador, Carta carta) : base(idJogador)
        {
            Carta = carta;
        }
    }
}

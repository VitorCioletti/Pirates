namespace Piratas.Protocolo.Cliente.Acoes
{
    using System;

    public class DescerCarta : Pacote
    {
        public string IdCarta { get; private set; }

        public DescerCarta(Guid idJogador, string idCarta) : base(idJogador)
        {
            IdCarta = idCarta;
        }
    }
}

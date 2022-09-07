namespace Piratas.Protocolo.Cliente.Acoes.Resultante
{
    using System;
    using Cartas;

    public class AfogarTripulante : Pacote
    {
        public Tripulante Tripulante { get; private set; }

        public AfogarTripulante(Guid idJogador, Tripulante tripulante) : base(idJogador)
        {
            Tripulante = tripulante;
        }
    }
}

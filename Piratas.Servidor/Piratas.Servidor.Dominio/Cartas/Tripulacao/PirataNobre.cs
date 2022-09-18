
namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class PirataNobre : Tripulante
    {
        public int Tesouros { get; private set; } = 1;

        public PirataNobre() => Tiros = 0;
    }
}

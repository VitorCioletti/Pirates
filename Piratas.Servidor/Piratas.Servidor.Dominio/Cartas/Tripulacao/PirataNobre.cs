
namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class PirataNobre : Tripulacao
    {
        public int Tesouros { get; private set; } = 1;

        public PirataNobre(string nome) : base(nome) => Tiros = 0;
    }
}

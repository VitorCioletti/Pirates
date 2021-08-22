
namespace Piratas.Servidor.Regras.Cartas.Tripulacao
{
    using Tipos;

    public class PirataNobre : Tripulacao
    {
        public int Tesouros = 1;

        public PirataNobre(string nome) : base(nome) => Tiros = 0;
    }
}
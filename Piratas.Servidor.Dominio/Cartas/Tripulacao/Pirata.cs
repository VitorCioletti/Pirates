namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class Pirata : Tripulacao
    {
        public Pirata(string nome) : base(nome) => Tiros = 1;
    }
}
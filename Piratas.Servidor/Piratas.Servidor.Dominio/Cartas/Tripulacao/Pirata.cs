namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class Pirata : Tripulante
    {
        public Pirata(string nome) : base(nome) => Tiros = 1;
    }
}

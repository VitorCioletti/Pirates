namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    using Jogadas;

    public class Pirata : Tripulacao
    {
        public Pirata(string nome) : base(nome) => Tiros = 1;
    }
}
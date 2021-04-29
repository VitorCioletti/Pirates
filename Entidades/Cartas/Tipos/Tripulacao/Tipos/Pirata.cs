namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    using Acoes;

    public class Pirata : Tripulacao
    {
        public Pirata(string nome) : base(nome) => Tiros = 1;
    }
}
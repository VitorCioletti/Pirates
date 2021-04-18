namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    using Jogadas;

    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome) => Tiros = 0;
    }
}
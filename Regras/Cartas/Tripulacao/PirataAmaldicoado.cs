namespace ServidorPiratas.Regras.Cartas.Tripulacao
{
    using Tipos;

    public class PirataAmaldicoado : Tripulacao
    {
        public PirataAmaldicoado(string nome) : base(nome) => Tiros = -1;
    }
}
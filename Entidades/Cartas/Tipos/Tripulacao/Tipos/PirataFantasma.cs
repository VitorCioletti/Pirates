namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    using Acoes;

    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome) => Tiros = 0;
    }
}
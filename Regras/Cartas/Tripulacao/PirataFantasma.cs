namespace ServidorPiratas.Regras.Cartas.Tripulacao
{
    using Tipos;

    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome)
        {
            Tiros = 0;
            PermiteAfogamento = false;
        }
    }
}
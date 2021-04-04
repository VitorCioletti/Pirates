namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome) 
        { 
            Tiros = 0;
        }
    }
}
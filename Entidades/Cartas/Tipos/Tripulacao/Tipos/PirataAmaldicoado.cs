namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    public class PirataAmaldicoado : Tripulacao
    {
        public PirataAmaldicoado(string nome) : base(nome) 
        { 
            Tiros = -1;
        }
    }
}
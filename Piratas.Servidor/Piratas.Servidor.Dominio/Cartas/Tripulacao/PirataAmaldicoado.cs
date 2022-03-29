namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class PirataAmaldicoado : Tripulante
    {
        public PirataAmaldicoado(string nome) : base(nome) => Tiros = -1;
    }
}

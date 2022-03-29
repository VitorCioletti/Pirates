namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class PirataFantasma : Tripulante
    {
        public PirataFantasma(string nome) : base(nome)
        {
            Tiros = 0;
            Afogavel = false;
        }
    }
}

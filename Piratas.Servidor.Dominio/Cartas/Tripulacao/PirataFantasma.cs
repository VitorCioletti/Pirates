namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Tipos;

    public class PirataFantasma : Tripulacao
    {
        public PirataFantasma(string nome) : base(nome)
        {
            Tiros = 0;
            Afogavel = false;
        }
    }
}
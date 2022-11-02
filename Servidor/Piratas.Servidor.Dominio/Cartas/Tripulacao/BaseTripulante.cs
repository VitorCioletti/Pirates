namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    public abstract class BaseTripulante : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        protected BaseTripulante()
        {
            Afogavel = true;
        }
    }
}

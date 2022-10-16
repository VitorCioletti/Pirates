namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    public abstract class Tripulante : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        protected Tripulante()
        {
            Afogavel = true;
        }
    }
}

namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    public abstract class Tripulante : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        public Tripulante()
        {
            Afogavel = true;
        }
    }
}

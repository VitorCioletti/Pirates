namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    public abstract class BaseCrewMember : Card
    {
        public int Shots { get; protected set; }

        public bool Drownable { get; protected set; }

        protected BaseCrewMember()
        {
            Drownable = true;
        }
    }
}

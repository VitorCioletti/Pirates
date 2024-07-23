namespace Pirates.Server.Domain.Card.Crew
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

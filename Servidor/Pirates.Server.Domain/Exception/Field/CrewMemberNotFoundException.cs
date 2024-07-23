namespace Pirates.Server.Domain.Exception.Field
{
    public class CrewMemberNotFoundException : BaseFieldException
    {
        public CrewMemberNotFoundException()
            : base("crew-member-not-found", "Crew member not found.")
        {
        }
    }
}

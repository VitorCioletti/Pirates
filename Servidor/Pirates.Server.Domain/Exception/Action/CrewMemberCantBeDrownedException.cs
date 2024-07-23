namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class CrewMemberCantBeDrownedException : BaseActionException
    {
        private const string _exceptionId = "crew-member-cant-be-drowned";

        public CrewMemberCantBeDrownedException(BaseAction action, string crewId)
            : base(action, _exceptionId, $"Crew member \"{crewId}\" cant be drowned.")
        {
        }

        public CrewMemberCantBeDrownedException(BaseAction action, string crewId, string idOriginCard)
            : base(
                action,
                _exceptionId,
                $"Crew member \"{crewId}\" can't be drowned by \"{idOriginCard}\".")
        {
        }
    }
}

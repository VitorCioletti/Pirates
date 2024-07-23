namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class DoesNotHaveCrewMemberException : BaseActionException
    {
        public DoesNotHaveCrewMemberException(BaseAction action, string playerId) :
            base(action, "does-not-have-crew-member", $"Player \"{playerId}\" does not have crew member.")
        {
        }
    }
}

namespace Pirates.Server.Domain.Exception.Action
{
    using Domain.Action;

    public class NoCrewMemberCanBeDrownedException : BaseActionException
    {
        public NoCrewMemberCanBeDrownedException(BaseAction action, string playerId)
            : base(
                action,
                "no-crew-member-can-be-drawned",
                $"Player\"{playerId}\" does not have any drownable crew member.")
        {
        }
    }
}

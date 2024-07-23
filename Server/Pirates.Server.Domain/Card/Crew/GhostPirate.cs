namespace Pirates.Server.Domain.Card.Crew
{
    using System.Collections.Generic;
    using Action;

    public class GhostPirate : BaseCrewMember
    {
        public GhostPirate()
        {
            Shots = 0;
            Drownable = false;
        }

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field targetField = action.Target.Field;

            targetField.Add(this);

            return null;
        }

    }
}

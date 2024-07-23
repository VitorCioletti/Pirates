namespace Pirates.Server.Domain.Card.Crew
{
    using System.Collections.Generic;
    using Action;

    public class CursedPirate : BaseCrewMember
    {
        public CursedPirate() => Shots = -1;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field targetField = action.Target.Field;

            targetField.Add(this);

            return null;
        }
    }
}

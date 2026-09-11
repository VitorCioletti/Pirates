namespace Pirates.Server.Domain.Card.Crew
{
    using System.Collections.Generic;
    using Action;

    public class Pirate : BaseCrewMember
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field starterField = action.Starter.Field;

            starterField.Add(this);

            return null;
        }
    }
}

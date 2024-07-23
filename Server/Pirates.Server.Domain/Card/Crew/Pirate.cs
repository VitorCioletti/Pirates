namespace Pirates.Server.Domain.Card.Crew
{
    using System.Collections.Generic;
    using Action;

    public class Pirate : BaseCrewMember
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field fieldRealizador = action.Starter.Field;

            fieldRealizador.Add(this);

            return null;
        }
    }
}

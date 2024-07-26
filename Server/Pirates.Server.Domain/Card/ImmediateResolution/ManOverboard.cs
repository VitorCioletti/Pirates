namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;

    public class ManOverboard : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player target = action.Target;

            var drownCrewMember = new DrownCrewMember(action, action.Starter, target);

           return new List<BaseAction> {drownCrewMember};
        }
   }
}

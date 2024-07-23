namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using System.Linq;
    using Action;
    using Action.Resultant;

    public class Spyglass : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            List<string> targetCards = action.Target.Hand.GetAll().Select(c => c.Id.ToString()).ToList();

            var discardCard = new DiscardCard(
                action,
                action.Starter,
                action.Target,
                targetCards);

            return new List<BaseAction> {discardCard};
        }
    }
}

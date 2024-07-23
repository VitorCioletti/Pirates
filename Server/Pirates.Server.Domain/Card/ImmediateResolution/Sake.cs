namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using Action;
    using Passive;

    public class Sake : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Hand targetHand = action.Target.Hand;
            Hand starterHand = action.Starter.Hand;

            (Hand handPlayerWhoStole, Hand handPlayerWhoWasStolen) =
                targetHand.Exists<TrapChest>()
                    ? (targetHand, starterHand)
                    : (starterHand, targetHand);

            Card stolenCard = handPlayerWhoWasStolen.GetAny();

            handPlayerWhoWasStolen.Remove(stolenCard);
            handPlayerWhoStole.Add(stolenCard);

            return null;
        }
    }
}

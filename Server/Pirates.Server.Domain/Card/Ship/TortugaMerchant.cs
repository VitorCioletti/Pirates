namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using Action;
    using Action.Immediate;
    using Action.Primary;

    public class TortugaMerchant : BaseShip
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            var buyCard = new BuyCard(action.Starter);

            var copyPrimmary = new CopyPrimmary(action.Starter, buyCard);
            var resultantActions = new List<BaseAction> {copyPrimmary};

            return resultantActions;
        }
    }
}

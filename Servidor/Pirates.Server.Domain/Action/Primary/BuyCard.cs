namespace Pirates.Server.Domain.Action.Primary
{
    using System.Collections.Generic;
    using Card;
    using Card.Event;

    public class BuyCard : BasePrimaryAction
    {
        public BuyCard(Player player) : base(player) { }

        public override List<BaseAction> ApplyRule(Table table)
        {
            Card boughtCard = table.CentralDeck.GetTop();

            if (boughtCard is BaseEvent)
                return boughtCard.ApplyEffect(this, table);

            Starter.Hand.Add(boughtCard);

            return null;
        }
    }
}

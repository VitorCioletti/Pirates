namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;
    using Deck;

    public class Diver : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            DiscardDeck discardDeck = table.DiscardDeck;

            var chooseCardInDeck = new ChooseCardInDeck(
                action,
                action.Starter,
                discardDeck,
                discardDeck.GetAll<Card>());

            return new List<BaseAction> {chooseCardInDeck};
        }
    }
}

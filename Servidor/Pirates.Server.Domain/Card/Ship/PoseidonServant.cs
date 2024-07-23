namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;
    using Deck;

    public class PoseidonServant : BaseShip
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

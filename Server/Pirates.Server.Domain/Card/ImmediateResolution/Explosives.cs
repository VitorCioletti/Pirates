namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;
    using Deck;

    public class Explosives : BaseImmediateResolution
    {
        private const int _cartasObtidas = 3;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            List<Player> players = table.Players;
            CentralDeck centralDeck = table.CentralDeck;

            List<Card> cards = centralDeck.GetTop(_cartasObtidas);

            var distributeCards = new DistributeCards(
                action,
                action.Starter,
                players,
                cards);

            return new List<BaseAction> {distributeCards};
        }
    }
}

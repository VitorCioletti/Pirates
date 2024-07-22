namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Baralhos;

    public class Rum : BaseImmediateResolution
    {
        private readonly int _cardsToBuy = 2;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Hand starterHand = action.Starter.Hand;
            CentralDeck centralDeck = table.CentralDeck;

            List<Card> boughtCards = centralDeck.GetTop(_cardsToBuy);
            starterHand.Add(boughtCards);

            return null;
        }
    }
}

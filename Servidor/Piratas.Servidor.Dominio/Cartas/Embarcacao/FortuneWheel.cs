namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class FortuneWheel : BaseShip
    {
        private readonly int _cardsToLook = 2;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            CentralDeck centralDeck = table.CentralDeck;

            List<Card> cards = centralDeck.GetTop(_cardsToLook);

            var lookAtDeckCards = new LookAtDeckCards(action, action.Starter, cards);

            return new List<BaseAction> {lookAtDeckCards};
        }
    }
}

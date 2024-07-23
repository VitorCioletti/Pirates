namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using Base;
    using Card;
    using Deck;
    using Enums;

    public class LookAtDeckCards : BaseResultantWithBooleanChoice
    {
        private List<Card> _cardsChoices { get; set; }

        public LookAtDeckCards(BaseAction origin, Player starter, List<Card> cardsChoices)
            : base(origin, starter, ChoiceType.Card)
        {
            _cardsChoices = cardsChoices;
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            CentralDeck centralDeck = table.CentralDeck;

            if (BooleanChoice)
                centralDeck.PushTop(_cardsChoices);
            else
                centralDeck.PushBottom(_cardsChoices);

            return null;
        }
    }
}

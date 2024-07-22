namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Baralhos;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Enums;

    public class ChooseCardInDeck : BaseResultantWithChoiceList
    {
        private readonly BaseDeck _baseDeck;

        private readonly List<Card> _cardChoices;

        public ChooseCardInDeck(
            BaseAction origin,
            Player starter,
            BaseDeck baseDeck,
            List<Card> cardChoices)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cardChoices.GetIds())
        {
            _baseDeck = baseDeck;
            _cardChoices = cardChoices;
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            Card chosenCard = _cardChoices.First(c => c.Id.ToString() == choice);

            Starter.Hand.Add(chosenCard);

            _cardChoices.Remove(chosenCard);
            _baseDeck.PushBottom(_cardChoices);

            return null;
        }
    }
}

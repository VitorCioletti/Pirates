namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Base;
    using Enums;

    public class ChooseCardAtTheHand : BaseResultantWithChoiceList
    {
        private readonly Action<Card> _onChoosing;

        public ChooseCardAtTheHand(
            BaseAction origin,
            Player starter,
            List<string> cardsChoices,
            Action<Card> onChoosing)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cardsChoices)
        {
            _onChoosing = onChoosing;
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            Card chosenCard = Starter.Hand.GetById(choice);

            _onChoosing(chosenCard);

            return null;
        }
    }
}

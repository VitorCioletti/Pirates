namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using Base;
    using Card;
    using Card.Treasure;
    using Enums;
    using Exception.Action;

    public class DiscardCard : BaseResultantWithChoiceList
    {
        public DiscardCard(
            BaseAction origin,
            Player starter,
            Player target,
            List<string> cardsToChoose)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cardsToChoose,
                target: target)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices[0];

            Card chosenCard = Target.Hand.GetById(choice);

            if (chosenCard.GetType() == typeof(Treasure))
                throw new ForbiddenToDrawCardException(this, chosenCard);

            Target.Hand.Remove(chosenCard);
            table.DiscardDeck.PushTop(chosenCard);

            return null;
        }
    }
}

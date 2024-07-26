namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Card;
    using Card.Extension;
    using Enums;

    public class StealCard : BaseResultantWithChoiceList
    {
        public StealCard(BaseAction origin, Player starter, Player target)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                target.Hand.GetAll<Card>().GetIds())
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            Card stolenCard = Target.Hand.GetById(choice);

            Starter.Hand.Add(stolenCard);
            Target.Hand.Remove(stolenCard);

            return null;
        }
    }
}

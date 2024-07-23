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
        public StealCard(BaseAction origin, Player starter, Player alvo)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                alvo.Hand.GetAll<Card>().GetIds())
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string escolha = Choices.First();

            Card cardRoubada = Target.Hand.GetById(escolha);

            Starter.Hand.Add(cardRoubada);
            Target.Hand.Remove(cardRoubada);

            return null;
        }
    }
}

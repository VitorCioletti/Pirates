namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using Base;
    using Card;
    using Card.Duel;
    using Card.Extension;
    using Card.SurpriseDuel;
    using Enums;
    using Immediate;

    public class DrawDuelAnswerCard : BaseResultantWithChoiceList
    {
        public DrawDuelAnswerCard(
            BaseAction origin,
            Player starter,
            Player target)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                target.Hand.GetAll<Cannon>().GetIds(),
                target: target)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            var resultantActions = new List<BaseAction>();

            foreach (string chosenCards in Choices)
            {
                Card duelCard = Target.Hand.GetById(chosenCards);

                duelCard.ApplyEffect(this, table);
            }

            bool starterHasSurpriseDuel = Starter.Hand.Exists<SurpriseDuel>();
            bool targetHasSurpriseDuel = Target.Hand.Exists<SurpriseDuel>();

            var calculateDuelResult = new CalculateDuelResult(Starter, Target);

            if (!starterHasSurpriseDuel && !targetHasSurpriseDuel)
                resultantActions.Add(calculateDuelResult);

            else
            {
                table.RegisterImmediateAfterResultants(calculateDuelResult);

                if (starterHasSurpriseDuel)
                {
                    var drawSurpriseDuelCard = new DrawSurpriseDuelCard(this, Starter);

                    resultantActions.Add(drawSurpriseDuelCard);
                }

                if (targetHasSurpriseDuel)
                {
                    var drawSurpriseDuelCard = new DrawSurpriseDuelCard(this, Target);

                    resultantActions.Add(drawSurpriseDuelCard);
                }
            }

            return resultantActions;
        }
    }
}

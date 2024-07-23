namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Card.Crew;
    using Card.Extension;
    using Card.ImmediateResolution;
    using Enums;
    using Exception.Action;
    using Primary;

    public class DrownCrewMember : BaseResultantWithChoiceList
    {
        public DrownCrewMember(
            BaseAction origin,
            Player starter,
            Player target)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                target.Hand.GetAll<BaseCrewMember>().GetIds(),
                target: target)
        {
            List<BaseCrewMember> crew = target.Field.Crew;

            if (crew.Count == 0)
                throw new DoesNotHaveCrewMemberException(this, target.Id);

            if (crew.All(t => !t.Drownable))
                throw new NoCrewMemberCanBeDrownedException(this, target.Id);
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices.First();

            var chosenCrewMember = (BaseCrewMember)Target.Hand.GetById(choice);

            if (Origin is DrawCard drawCard)
            {
                if (drawCard.Card is ManOverboard manOverboard)
                {
                    if (chosenCrewMember is NoblePirate)
                    {
                        throw new CrewMemberCantBeDrownedException(this, chosenCrewMember.Id, manOverboard.Id);
                    }
                }
            }

            if (!chosenCrewMember.Drownable)
                throw new CrewMemberCantBeDrownedException(this, chosenCrewMember.Id);

            Target.Field.Remove(chosenCrewMember);
            table.DiscardDeck.PushTop(chosenCrewMember);

            return null;
        }
    }
}

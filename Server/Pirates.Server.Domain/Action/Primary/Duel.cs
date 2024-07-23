namespace Pirates.Server.Domain.Action.Primary
{
    using System.Collections.Generic;
    using Card.Duel;
    using Card.Extension;
    using Exception.Action;
    using Resultant;
    using Resultant.Enums;

    public class Duel : BasePrimaryAction
    {
        public Card.Duel.Duel StarterCard { get; private set; }

        public Duel(Player starter, Player target, Card.Duel.Duel starterCard) : base(starter, target) =>
            StarterCard = starterCard;

        public override List<BaseAction> ApplyRule(Table table)
        {
            List<Cannon> cannons = Starter.Hand.GetAll<Cannon>();

            if (cannons.Count == 0)
                throw new DoesNotHaveDuelCardException(this);

            table.EnterDuelMode();

            var chooseCannonToStartDuelling = new ChooseCannonToStartDuelling(
                this,
                Starter,
                ChoiceType.Card,
                cannons.GetIds()
            );

            var resultantActions = new List<BaseAction> {chooseCannonToStartDuelling};

            return resultantActions;
        }
    }
}

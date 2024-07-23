namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using Base;
    using Card;
    using Card.Extension;
    using Enums;

    public class LookAtPlayerCards : BaseResultantWithBooleanListChoice
    {
        public LookAtPlayerCards(
            BaseAction origin,
            Player starter,
            List<Card> cards)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cards.GetIds()) {}

        public override List<BaseAction> ApplyRule(Table table) => null;
    }
}

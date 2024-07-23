namespace Pirates.Server.Domain.Card.Duel
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;
    using Exception.Card;

    public class Helmsman : Duel
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            if (action is not DrawDuelAnswerCard)
                throw new CanOnlyBeUsedInDuelException(this);

            table.EndDuelMode();

            return null;
        }
    }
}

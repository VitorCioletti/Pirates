namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using System.Linq;
    using Action;
    using Action.Immediate;
    using Action.Primary;
    using Action.Resultant;
    using Duel;
    using Exception.Card;
    using Duel = Duel.Duel;

    public class Parrot : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            List<Player> allPlayers = table.Players;

            BaseAction lastAction = table.ActionHistory.FirstOrDefault(
                a => a.Turn == action.Turn && a is DrawCard or Action.Primary.Duel);

            if (lastAction == null)
                throw new HasNoValidActionException(this);

            var resultantActions = new List<BaseAction>();

            switch (lastAction)
            {
                case DrawCard drawCard:
                    Card cardToCopy = drawCard.Card;

                    bool notAllowedTypes = cardToCopy is not (BaseImmediateResolution or Cannon);

                    if (notAllowedTypes)
                        throw new ImpossibleToCopyException(this, cardToCopy);

                    foreach (List<BaseAction> playersActions in table.ProcessAction(lastAction).Values)
                    {
                        foreach (BaseAction availableAction in playersActions)
                        {
                            resultantActions.Add(availableAction);
                        }
                    }

                    break;

                case Action.Primary.Duel duel:
                    Player starter = duel.Starter;

                    Duel starterCard = duel.StarterCard;

                    List<string> otherPlayers =
                        allPlayers.Where(p => p != starter).Select(p => p.Id.ToString()).ToList();

                    var choosePlayer = new ChoosePlayer(
                        action,
                        starter,
                        otherPlayers,
                        Duel);

                    resultantActions.Add(choosePlayer);

                    List<BaseAction> Duel(BaseAction chosenAction, Player chosenPlayer)
                    {
                        var duelAction = new Action.Primary.Duel(starter, chosenPlayer, starterCard);
                        var copyPrimmary = new CopyPrimmary(starter, duelAction);

                        return new List<BaseAction> {copyPrimmary};
                    }

                    break;

                default:
                    throw new HasNoValidActionException(this);
            }

            return resultantActions;
        }
    }
}

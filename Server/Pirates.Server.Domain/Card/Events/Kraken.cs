namespace Pirates.Server.Domain.Card.Events
{
    using System.Collections.Generic;
    using System.Linq;
    using Pirates.Server.Domain.Action;
    using Pirates.Server.Domain.Action.Immediate;
    using Pirates.Server.Domain.Action.Resultant;
    using Pirates.Server.Domain.Card.Crew;

    public class Kraken : BaseEvent
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            List<Player> allPlayers = table.Players;

            var resultantActions = new List<BaseAction>();

            foreach (Player player in allPlayers)
            {
                bool hasShip = player.Field.Ship != null;
                bool hasAnyCrew = player.Field.Crew.Count == 0;

                var drownCrewMember = new DrownCrewMember(action, player, player);
                var damageShip = new DamageShip(player);

                if (!hasShip && !hasAnyCrew)
                    continue;

                if (hasShip && hasAnyCrew)
                {
                    var chooseAction = new ChooseAction(
                        action,
                        player,
                        drownCrewMember,
                        damageShip);

                    resultantActions.Add(chooseAction);
                }
                else if (!hasShip)
                {
                    List<BaseCrewMember> drownableCrewMembers = player.Field.Crew.Where(t => t.Drownable).ToList();

                    if (drownableCrewMembers.Count == 0)
                        continue;

                    if (drownableCrewMembers.Count == 1)
                        player.Field.DrownCrew();

                    else
                        resultantActions.Add(drownCrewMember);
                }
                else
                {
                    player.Field.DamageShip();
                }
            }

            return resultantActions;
        }
    }
}

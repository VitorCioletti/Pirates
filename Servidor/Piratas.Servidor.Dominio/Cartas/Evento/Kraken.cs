namespace Piratas.Servidor.Dominio.Cartas.Evento
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Resultante;
    using Tripulacao;

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

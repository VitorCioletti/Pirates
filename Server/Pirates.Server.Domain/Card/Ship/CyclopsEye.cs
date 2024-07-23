namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using System.Linq;
    using Action;
    using Action.Resultant;

    public class CyclopsEye : BaseShip
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player starter = action.Starter;

            List<string> otherPlayers =
                table.Players.Where(p => p != starter).ToList().Select(p => p.Id.ToString()).ToList();

            var choosePlayer = new ChoosePlayer(
                action,
                starter,
                otherPlayers,
                LookCards);

            return new List<BaseAction> {choosePlayer};

            List<BaseAction> LookCards(BaseAction chosenAction, Player player)
            {
                var lookAtPlayerCards =
                    new LookAtPlayerCards(chosenAction, starter, player.Hand.GetAll());

                return new List<BaseAction> {lookAtPlayerCards};
            }
        }
    }
}

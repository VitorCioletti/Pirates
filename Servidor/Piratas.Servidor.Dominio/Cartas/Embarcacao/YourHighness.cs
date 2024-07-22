namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;

    public class YourHighness : BaseShip
    {
        private const int _minimumCardAtHand = 5;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player starter = action.Starter;
            List<Player> allPlayers = table.Players;

            List<Player> playersToChoose =
                allPlayers.Where(j => j.Hand.GetCardQuantity() >= _minimumCardAtHand && j != starter).ToList();

            List<string> playersIds = playersToChoose.Select(j => j.Id.ToString()).ToList();

            var choosePlayer = new ChoosePlayer(
                action,
                starter,
                playersIds,
                StealCard);

            return new List<BaseAction> {choosePlayer};

            List<BaseAction> StealCard(BaseAction chosenAction, Player target)
            {
                var stealCard = new StealCard(chosenAction, starter, target);

                return new List<BaseAction> {stealCard};
            }
        }
    }
}

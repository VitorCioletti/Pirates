namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using Action;

    public class WingedDutch : BaseShip
    {
        private const int _treasuresToWin = 4;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player starter = action.Starter;

            int allTreasurePoints = starter.CalculateTreasurePoints();

            if (allTreasurePoints >= _treasuresToWin)
                table.EndGame(starter);

            return null;
        }
    }
}

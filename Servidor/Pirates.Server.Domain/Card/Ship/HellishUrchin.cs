namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using Action;

    public class HellishUrchin : BaseShip
    {
        public int Shots { get; private set; } = 3;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table) => null;
    }
}

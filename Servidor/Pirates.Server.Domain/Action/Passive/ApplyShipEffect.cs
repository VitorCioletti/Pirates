namespace Pirates.Server.Domain.Action.Passive
{
    using System.Collections.Generic;
    using Card.Ship;

    public class ApplyShipEffect : BasePassiveAction
    {
        private BaseShip BaseShip { get; set; }

        public ApplyShipEffect(Player starter, BaseShip baseShip) : base(starter) =>
            BaseShip = baseShip;

        public override List<BaseAction> ApplyRule(Table table)
        {
            return BaseShip.ApplyEffect(this, table);
        }
    }
}

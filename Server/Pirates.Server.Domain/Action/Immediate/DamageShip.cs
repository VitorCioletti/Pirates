namespace Pirates.Server.Domain.Action.Immediate
{
    using System.Collections.Generic;

    public class DamageShip : BaseImmediateAction
    {
        public DamageShip(Player starter) : base(starter)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            Starter.Field.DamageShip();

            return null;
        }
    }
}
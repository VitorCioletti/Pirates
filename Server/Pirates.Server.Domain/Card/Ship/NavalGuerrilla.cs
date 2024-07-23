namespace Pirates.Server.Domain.Card.Ship
{
    using System.Collections.Generic;
    using Action;

    public class NavalGuerrilla : BaseShip
    {
        public int AdditionalShots { get; private set; } = 2;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table) => null;
    }
}

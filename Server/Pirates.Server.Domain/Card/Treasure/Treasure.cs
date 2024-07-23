namespace Pirates.Server.Domain.Card.Treasure
{
    using System.Collections.Generic;
    using Action;

    public class Treasure : Card
    {
        public int Value { get; protected set; }

        public Treasure(int value) => Value = value;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table) => null;
    }
}

namespace Pirates.Server.Domain.Card.Duel
{
    using System;
    using System.Collections.Generic;
    using Action;

    public class Cannon : Duel
    {
        public int Shots { get; private set; }

        public Cannon()
        {
            var random = new Random();

            Shots = random.Next(1, 2);
        }

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field starterField = action.Starter.Field;

            starterField.Add(this);

            return null;
        }
    }
}

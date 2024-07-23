namespace Pirates.Server.Domain.Action
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseAction
    {
        public string Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public Player Starter { get; private set; }

        public Player Target { get; private set; }

        public int Turn { get; set; }

        protected BaseAction(Player starter, Player target = null)
        {
            Id = GetType().ToString();
            DateTime = DateTime.UtcNow;

            Starter = starter;
            Target = target;
        }

        public abstract List<BaseAction> ApplyRule(Table table);
    }
}

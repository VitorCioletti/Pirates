namespace Pirates.Server.Domain.Card
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Action;

    public abstract class Card
    {
        public string Id { get; private set; }

        protected Card()
        {
            string typeName = GetType().Name;

            string id = Regex.Replace(typeName, @"([a-z0–9])([A-Z])", "$1-$2").ToLowerInvariant();

            Id = id;
        }

        public abstract List<BaseAction> ApplyEffect(BaseAction action, Table table);
    }
}

namespace Pirates.Server.Domain.Card.SurpriseDuel
{
    using System.Collections.Generic;
    using Action;
    using Duel;

    public abstract class SurpriseDuel : Duel
    {
        public int Shots { get; private set; }

        protected SurpriseDuel() => Shots = 1;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field fieldRealizador = action.Starter.Field;

            fieldRealizador.Add(this);

            return null;
        }
    }
}

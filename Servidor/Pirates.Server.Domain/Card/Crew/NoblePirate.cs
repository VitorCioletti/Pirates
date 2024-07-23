namespace Pirates.Server.Domain.Card.Crew
{
    using System.Collections.Generic;
    using Action;

    public class NoblePirate : BaseCrewMember
    {
        public int Treasures { get; private set; } = 1;

        public NoblePirate() => Shots = 0;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field fieldRealizador = action.Starter.Field;

            fieldRealizador.Add(this);

            return null;
        }
    }
}

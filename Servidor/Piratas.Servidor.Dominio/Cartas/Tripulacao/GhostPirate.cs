namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class GhostPirate : BaseCrewMember
    {
        public GhostPirate()
        {
            Shots = 0;
            Drownable = false;
        }

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field targetField = action.Target.Field;

            targetField.Add(this);

            return null;
        }

    }
}

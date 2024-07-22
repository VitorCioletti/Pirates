namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class CursedPirate : BaseCrewMember
    {
        public CursedPirate() => Shots = -1;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field targetField = action.Target.Field;

            targetField.Add(this);

            return null;
        }
    }
}

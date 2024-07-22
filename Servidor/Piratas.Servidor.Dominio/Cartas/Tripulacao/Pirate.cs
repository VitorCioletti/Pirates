namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class Pirate : BaseCrewMember
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Field fieldRealizador = action.Starter.Field;

            fieldRealizador.Add(this);

            return null;
        }
    }
}

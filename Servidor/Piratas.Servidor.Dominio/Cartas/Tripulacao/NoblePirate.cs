namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

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

namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System.Collections.Generic;
    using Acoes;

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

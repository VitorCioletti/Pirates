namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Cartas.Tipos;
    using Enums;

    public class DrawSurpriseDuelCard : BaseResultantWithChoiceList
    {
        public DrawSurpriseDuelCard(BaseAction origin, Player starter)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                starter.Hand.GetAll<Duel>().GetIds(),
                2)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            foreach (string chosenCard in Choices)
            {
                Card surpriseDuel = Starter.Hand.GetById(chosenCard);

                surpriseDuel.ApplyEffect(this, table);
            }

            return null;
        }
    }
}

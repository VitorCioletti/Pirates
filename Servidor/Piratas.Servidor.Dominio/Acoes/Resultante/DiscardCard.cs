namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Tesouro;
    using Enums;
    using Excecoes.Acoes;

    public class DiscardCard : BaseResultantWithChoiceList
    {
        public DiscardCard(
            BaseAction origin,
            Player starter,
            Player target,
            List<string> cardsToChoose)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                cardsToChoose,
                target: target)
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            string choice = Choices[0];

            Card chosenCard = Target.Hand.GetById(choice);

            if (chosenCard.GetType() == typeof(Treasure))
                throw new ForbiddenToDrawCardException(this, chosenCard);

            Target.Hand.Remove(chosenCard);
            table.DiscardDeck.PushTop(chosenCard);

            return null;
        }
    }
}

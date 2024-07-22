namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Tesouro;

    public class IronHull : BaseShip
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player starter = action.Starter;

            List<string> treasuresAtHand = starter.Hand
                .GetAll<Treasure>()
                .OfType<Card>()
                .Select(c => c.Id)
                .ToList();

            var chooseCardAtTheHand = new ChooseCardAtTheHand(
                action,
                starter,
                treasuresAtHand,
                OnChoice);

            var resultantActions = new List<BaseAction> {chooseCardAtTheHand};

            return resultantActions;

            void OnChoice(Card card)
            {
                starter.Hand.Remove(card);
                starter.Field.AddProtected(card);
            }
        }
    }
}

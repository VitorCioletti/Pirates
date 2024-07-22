namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class PoseidonServant : BaseShip
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            DiscardDeck discardDeck = table.DiscardDeck;

            var chooseCardInDeck = new ChooseCardInDeck(
                action,
                action.Starter,
                discardDeck,
                discardDeck.GetAll<Card>());

            return new List<BaseAction> {chooseCardInDeck};
        }
    }
}

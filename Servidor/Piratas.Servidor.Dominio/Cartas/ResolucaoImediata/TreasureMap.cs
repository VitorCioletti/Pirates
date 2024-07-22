namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class TreasureMap : BaseImmediateResolution
    {
        private const int _cardsToChoose = 4;

        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            CentralDeck centralDeck = table.CentralDeck;

            List<Card> choices = centralDeck.GetTop(_cardsToChoose);

            var chooseCardInDeck = new ChooseCardInDeck(
                action,
                action.Starter,
                centralDeck,
                choices);

            return new List<BaseAction> {chooseCardInDeck};
        }
    }
}

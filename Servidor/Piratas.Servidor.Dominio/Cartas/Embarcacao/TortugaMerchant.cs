namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Primaria;

    public class TortugaMerchant : BaseShip
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            var buyCard = new BuyCard(action.Starter);

            var copyPrimmary = new CopyPrimmary(action.Starter, buyCard);
            var resultantActions = new List<BaseAction> {copyPrimmary};

            return resultantActions;
        }
    }
}

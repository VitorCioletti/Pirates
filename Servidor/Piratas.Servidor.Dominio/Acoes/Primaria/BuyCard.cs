namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Evento;

    public class BuyCard : BasePrimaryAction
    {
        public BuyCard(Player player) : base(player) { }

        public override List<BaseAction> ApplyRule(Table table)
        {
            Card boughtCard = table.CentralDeck.GetTop();

            if (boughtCard is BaseEvent)
                return boughtCard.ApplyEffect(this, table);

            Starter.Hand.Add(boughtCard);

            return null;
        }
    }
}

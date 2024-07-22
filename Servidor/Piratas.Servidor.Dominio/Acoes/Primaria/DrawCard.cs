namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Passivo;
    using Cartas.Tesouro;
    using Excecoes.Acoes;

    public class DrawCard : BasePrimaryAction
    {
        public Card Card { get; private set; }

        public DrawCard(Player player, Card card) : base(player) => Card = card;

        public override List<BaseAction> ApplyRule(Table table)
        {
            if (Card is Treasure or BasePassive)
                throw new ForbiddenToDrawCardException(this, Card);

            List<BaseAction> resultantActions = Card.ApplyEffect(this, table);

            Starter.Hand.Remove(Card);
            table.DiscardDeck.PushTop(Card);

            return resultantActions;
        }
    }
}

namespace Pirates.Server.Domain.Action.Resultant
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Card;
    using Card.Extension;
    using Enums;

    public class DistributeCards : BaseResultantWithDictionaryChoice
    {
        public DistributeCards(
            BaseAction origin,
            Player starter,
            IEnumerable<Player> jogadores,
            IEnumerable<Card> cartas)
            : base(
                origin,
                starter,
                ChoiceType.Card,
                ChoiceType.Player,
                ChoiceType.Card,
                2,
                cartas.GetIds(),
                jogadores.Select(j => j.Id.ToString()).ToList())
        {
        }

        public override List<BaseAction> ApplyRule(Table table)
        {
            foreach ((string playerId, string cardId) in Choices)
            {
                Player player = table.Players.First(j => j.Id.ToString() == playerId);
                Card card = player.Hand.GetById(cardId);

                player.Hand.Add(card);
            }

            return null;
        }
    }
}

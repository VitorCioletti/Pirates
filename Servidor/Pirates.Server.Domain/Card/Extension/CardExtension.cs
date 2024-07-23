namespace Pirates.Server.Domain.Card.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    public static class CardExtension
    {
        public static List<string> GetIds(this IEnumerable<Card> cards)
        {
            return cards.Select(c => c.Id).ToList();
        }
    }
}

namespace Pirates.Protocol.Match.Server
{
    public class Event
    {
        public EventLocation Location { get; private set; }

        public string CardId { get; private set; }

        public bool Added { get; private set; }

        public Event(EventLocation location, string cardId, bool added)
        {
            Location = location;
            CardId = cardId;
            Added = added;
        }
    }
}

namespace Pirates.Protocol.Match.Client.Choice
{
    using System.Collections.Generic;

    public class ClientBooleanChoiceDictionary : BaseChoice
    {
        public Dictionary<string, string> Choices { get; private set; }

        public ClientBooleanChoiceDictionary(ChoiceType type, Dictionary<string, string> choices) : base(type)
        {
            Choices = choices;
        }
    }
}

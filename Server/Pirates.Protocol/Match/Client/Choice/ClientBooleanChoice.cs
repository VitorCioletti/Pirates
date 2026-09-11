namespace Pirates.Protocol.Match.Client.Choice
{
    using System.Collections.Generic;

    public class ClientBooleanChoice : BaseChoice
    {
        public Dictionary<string, string> Choices { get; private set; }

        public ClientBooleanChoice(ChoiceType type, Dictionary<string, string> choices) : base(type)
        {
            Choices = choices;
        }
    }
}

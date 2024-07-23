namespace Pirates.Protocol.Match.Client.Choice
{
    using System.Collections.Generic;

    public class ClientChoiceList : BaseChoice
    {
        public List<string> Choices { get; private set; }

        public ClientChoiceList(ChoiceType type, List<string> choices) : base(type)
        {
            Choices = choices;
        }
    }
}

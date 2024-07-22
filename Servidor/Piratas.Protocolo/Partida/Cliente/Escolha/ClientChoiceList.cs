namespace Piratas.Protocolo.Partida.Cliente.Escolha
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

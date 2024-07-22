namespace Piratas.Protocolo.Partida.Servidor.Escolha
{
    using System.Collections.Generic;

    public class ServerChoicesLIst : BaseChoice
    {
        public List<string> Choices { get; private set; }

        public int ChoiceLimit { get; private set; }

        public ServerChoicesLIst(ChoiceType type, List<string> choices, int choiceLimit = 1) : base(type)
        {
            Choices = choices;
            ChoiceLimit = choiceLimit;
        }
    }
}

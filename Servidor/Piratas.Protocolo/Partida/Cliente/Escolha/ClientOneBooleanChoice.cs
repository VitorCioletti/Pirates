namespace Piratas.Protocolo.Partida.Cliente.Escolha
{
    public class ClientOneBooleanChoice : BaseChoice
    {
        public bool Choice { get; private set; }

        public ClientOneBooleanChoice(ChoiceType type, bool choice) : base(type)
        {
            Choice = choice;
        }
    }
}

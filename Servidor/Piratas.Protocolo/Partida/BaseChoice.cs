namespace Piratas.Protocolo.Partida
{
    public abstract class BaseChoice
    {
        public ChoiceType Type { get; private set; }

        protected BaseChoice(ChoiceType type)
        {
            Type = type;
        }
    }
}

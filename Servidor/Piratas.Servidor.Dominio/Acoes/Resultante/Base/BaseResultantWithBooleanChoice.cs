namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultantWithBooleanChoice : BaseResultant
    {
        protected bool BooleanChoice { get; private set; }

        protected BaseResultantWithBooleanChoice(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            Player target = null)
            : base(
                origin,
                starter,
                choiceType,
                target)
        {
            BooleanChoice = false;
        }

        public void FillChoice(bool choice)
        {
            BooleanChoice = choice;
        }
    }
}

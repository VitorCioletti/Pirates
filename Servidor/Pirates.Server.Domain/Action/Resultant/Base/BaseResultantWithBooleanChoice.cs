namespace Pirates.Server.Domain.Action.Resultant.Base
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

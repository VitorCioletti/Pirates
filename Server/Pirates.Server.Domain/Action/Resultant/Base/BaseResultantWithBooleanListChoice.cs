namespace Pirates.Server.Domain.Action.Resultant.Base
{
    using System.Collections.Generic;
    using Enums;

    public abstract class BaseResultantWithBooleanListChoice : BaseResultant
    {
        public List<string> Choices { get; private set; }

        protected bool Choice { get; private set; }

        protected BaseResultantWithBooleanListChoice(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            List<string> choices,
            Player target = null)
            : base(
                origin,
                starter,
                choiceType,
                target)
        {
            Choices = choices;
            Choice = false;
        }

        public void FillChoice(bool choice)
        {
            Choice = choice;
        }
    }
}

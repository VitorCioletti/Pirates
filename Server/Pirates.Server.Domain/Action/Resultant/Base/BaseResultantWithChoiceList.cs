namespace Pirates.Server.Domain.Action.Resultant.Base
{
    using System.Collections.Generic;
    using Enums;
    using Exception.Action;

    public abstract class BaseResultantWithChoiceList : BaseResultant
    {
        protected List<string> Choices { get; private set; }

        public List<string> Options { get; private set; }

        public int ChoiceLimit { get; private set; }

        protected BaseResultantWithChoiceList(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            List<string> options,
            int choiceLimit = 1,
            Player target = null)
            : base(
                origin,
                starter,
                choiceType,
                target)
        {
            ChoiceLimit = choiceLimit;
            Options = options;
        }

        public void FillChoices(List<string> choicesId)
        {
            if (choicesId.Count > ChoiceLimit)
                throw new ChoiceLimitReachedException(this, choicesId.Count);

            foreach (string choice in choicesId)
            {
                if (!Options.Contains(choice))
                    throw new ChoiceIsNotAnOptionException(this, choice);
            }

            Choices = choicesId;
        }
    }
}

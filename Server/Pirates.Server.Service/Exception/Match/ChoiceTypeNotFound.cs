namespace Pirates.Server.Service.Exception.Match
{
    public class ChoiceTypeNotFound : BaseMatchException
    {
        public ChoiceTypeNotFound(int choiceType) :
            base("choice-type-not-found", $"Choice type \"{choiceType}\" not found.")
        {
        }
    }
}

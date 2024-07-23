namespace Pirates.Server.Domain.Exception.Field
{
    public class EmptyCrewException : BaseFieldException
    {
        public EmptyCrewException() : base("empty-crew", "The crew is empty.")
        {
        }
    }
}

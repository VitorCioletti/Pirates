namespace Pirates.Server.Domain.Exception.Field
{
    public class FullCrewException : BaseFieldException
    {
        public FullCrewException() : base("full-crew", "The crew is full.")
        {
        }
    }
}

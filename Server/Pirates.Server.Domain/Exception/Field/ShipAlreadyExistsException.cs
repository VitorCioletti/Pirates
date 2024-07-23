namespace Pirates.Server.Domain.Exception.Field
{
    public class ShipAlreadyExistsException : BaseFieldException
    {
        public ShipAlreadyExistsException() : base("ship-already-exists", "There is a ship already in position.")
        {
        }
    }
}

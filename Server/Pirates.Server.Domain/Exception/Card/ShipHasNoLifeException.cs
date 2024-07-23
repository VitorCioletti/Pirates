namespace Pirates.Server.Domain.Exception.Card
{
    using Domain.Card.Ship;

    public class ShipHasNoLifeException : BaseCardException
    {
        public ShipHasNoLifeException(BaseShip baseShip)
            : base(baseShip, "ship-has-no-life", $"Ship \"{baseShip.Id}\" has no life.")
        {
        }
    }
}

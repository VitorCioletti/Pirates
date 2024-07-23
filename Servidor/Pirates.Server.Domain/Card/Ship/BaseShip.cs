namespace Pirates.Server.Domain.Card.Ship
{
    using Exception.Card;

    public abstract class BaseShip : Card
    {
        public int Life { get; private set; } = 3;

        public void TakeDamage(int dano)
        {
            if (Life == 0)
                throw new ShipHasNoLifeException(this);

            Life -= dano;
        }
    }
}

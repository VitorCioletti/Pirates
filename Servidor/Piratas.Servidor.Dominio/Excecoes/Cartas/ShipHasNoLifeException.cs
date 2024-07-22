namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas.Embarcacao;

    public class ShipHasNoLifeException : BaseCardException
    {
        public ShipHasNoLifeException(BaseShip baseShip)
            : base(baseShip, "ship-has-no-life", $"Ship \"{baseShip.Id}\" has no life.")
        {
        }
    }
}

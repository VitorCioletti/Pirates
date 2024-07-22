namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class HasNoValidActionException : BaseCardException
    {
        public HasNoValidActionException(Card card) : base(card, "has-no-valid-action", "Has no valid action.")
        {
        }
    }
}

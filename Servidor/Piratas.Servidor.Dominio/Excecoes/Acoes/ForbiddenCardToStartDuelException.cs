namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class ForbiddenCardToStartDuelException : BaseActionException
    {
        public ForbiddenCardToStartDuelException(BaseAction action, Card card)
            : base(action, "forbidden-card-to-start-duel", $"Card \"{card.Id}\" is not allowed to start a duel.")
        {
        }
    }
}

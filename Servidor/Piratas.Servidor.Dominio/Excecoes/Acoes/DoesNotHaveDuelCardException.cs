namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class DoesNotHaveDuelCardException : BaseActionException
    {
        public DoesNotHaveDuelCardException(BaseAction action)
            : base(
                action,
                "does-not-have-duel-card",
                $"Player \"{action.Starter.Id}\" does not have any duel card.")
        {
        }
    }
}

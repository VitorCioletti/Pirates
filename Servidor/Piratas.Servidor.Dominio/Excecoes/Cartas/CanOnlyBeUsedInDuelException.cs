namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class CanOnlyBeUsedInDuelException : BaseCardException
    {
        public CanOnlyBeUsedInDuelException(Card card)
            : base(
                card,
                "can-only-be-used-in-duel",
                $"Card \"{card.Id}\" can only be used in duel.")
        {
        }
    }
}

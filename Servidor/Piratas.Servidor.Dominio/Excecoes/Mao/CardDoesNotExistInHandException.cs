namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    using Dominio.Cartas;

    public class CardDoesNotExistInHandException : BaseHandException
    {
        public CardDoesNotExistInHandException(Card card)
            : base("card-does-not-exist-in-hand", $"Card \"{card.Id}\" does not exist in the hand.")
        {
        }
    }
}

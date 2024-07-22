namespace Piratas.Servidor.Dominio.Baralhos.Excecoes
{
    public class CardNotFoundInDiscardDeckException : BaseDeckException
    {
        public CardNotFoundInDiscardDeckException(string cardId) :
            base("card-not-found-in-discard-deck", $"Card \"{cardId}\" was not found in discard deck.")
        {
        }
    }
}

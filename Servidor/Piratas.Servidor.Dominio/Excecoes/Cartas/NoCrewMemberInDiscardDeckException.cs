namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class NoCrewMemberInDiscardDeckException : BaseCardException
    {
        public NoCrewMemberInDiscardDeckException(Card card)
            : base(card, "no-crew-member-in-discard-deck", "No crew member in discard deck.")
        {
        }
    }
}

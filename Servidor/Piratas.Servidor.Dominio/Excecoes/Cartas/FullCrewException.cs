namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas;

    public class FullCrewException : BaseCardException
    {
        public FullCrewException(Card card, Player player) : base(
            card,
            "full-crew",
            $"Player \"{player.Id}\" crew is full.")
        {
        }
    }
}

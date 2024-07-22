namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class ForbiddenToDrawCardException : BaseActionException
    {
        public ForbiddenToDrawCardException(BaseAction action, Card card)
            : base(action, "forbidden-to-draw-card", $"Forbidden to draw card of type \"{card.Id}\".")
        {
        }
    }
}

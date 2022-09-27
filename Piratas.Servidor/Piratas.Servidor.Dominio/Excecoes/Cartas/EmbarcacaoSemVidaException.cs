namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas.Tipos;

    public class EmbarcacaoSemVidaException : BaseCartaException
    {
        public EmbarcacaoSemVidaException(Embarcacao embarcacao)
            : base(embarcacao, "embarcacao-sem-vida", $"Embarcação \"{embarcacao.Id}\" sem vida.")
        {
        }
    }
}

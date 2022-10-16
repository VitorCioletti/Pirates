namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas.Tipos;

    public class EmbarcacaoSemVidaExcecao : BaseCartaExcecao
    {
        public EmbarcacaoSemVidaExcecao(Embarcacao embarcacao)
            : base(embarcacao, "embarcacao-sem-vida", $"Embarcação \"{embarcacao.Id}\" sem vida.")
        {
        }
    }
}

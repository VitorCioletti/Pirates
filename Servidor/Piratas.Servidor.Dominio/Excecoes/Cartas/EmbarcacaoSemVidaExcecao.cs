namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas.Embarcacao;

    public class EmbarcacaoSemVidaExcecao : BaseCartaExcecao
    {
        public EmbarcacaoSemVidaExcecao(BaseEmbarcacao baseEmbarcacao)
            : base(baseEmbarcacao, "embarcacao-sem-vida", $"Embarcação \"{baseEmbarcacao.Id}\" sem vida.")
        {
        }
    }
}

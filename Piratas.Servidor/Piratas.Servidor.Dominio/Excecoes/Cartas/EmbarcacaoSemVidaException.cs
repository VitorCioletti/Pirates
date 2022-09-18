namespace Piratas.Servidor.Dominio.Excecoes.Cartas
{
    using Dominio.Cartas.Tipos;

    public class EmbarcacaoSemVidaException : CartaException
    {
        public EmbarcacaoSemVidaException(Embarcacao embarcacao)
            : base(embarcacao, $"Embarcação \"{embarcacao.Id}\" sem vida.")
        {
        }
    }
}

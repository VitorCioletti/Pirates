namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class SemEmbarcacaoExcecao : BaseCampoExcecao
    {
        public SemEmbarcacaoExcecao() : base("sem-embarcacao", "Não há embarcação no campo.")
        {
        }
    }
}

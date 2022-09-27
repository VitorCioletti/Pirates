namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class SemEmbarcacaoException : BaseCampoException
    {
        public SemEmbarcacaoException() : base("sem-embarcacao", "Não há embarcação no campo.")
        {
        }
    }
}

namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class ExisteEmbarcacaoException : BaseCampoException
    {
        public ExisteEmbarcacaoException() : base("existe-embarcacao", "Jã existe uma embarcação no campo.")
        {
        }
    }
}

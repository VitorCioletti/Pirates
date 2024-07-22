namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class NoDuelException : BaseTableException
    {
        public NoDuelException() : base("no-duel", "There is no duel happening.")
        {
        }
    }
}

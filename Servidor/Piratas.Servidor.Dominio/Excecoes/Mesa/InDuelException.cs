namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class InDuelException : BaseTableException
    {
        public InDuelException() : base("in-duel", "Table already in duel.")
        {
        }
    }
}

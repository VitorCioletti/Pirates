namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public class EmptyHandException : BaseHandException
    {
        public EmptyHandException() : base("empty-hand", "Hand is empty.")
        {
        }
    }
}

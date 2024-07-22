namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class NoShipException : BaseFieldException
    {
        public NoShipException() : base("no-ship", "There is not ship.")
        {
        }
    }
}

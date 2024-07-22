namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class EmptyCrewException : BaseFieldException
    {
        public EmptyCrewException() : base("empty-crew", "The crew is empty.")
        {
        }
    }
}

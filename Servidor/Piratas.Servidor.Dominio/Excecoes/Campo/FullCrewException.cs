namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class FullCrewException : BaseFieldException
    {
        public FullCrewException() : base("full-crew", "The crew is full.")
        {
        }
    }
}

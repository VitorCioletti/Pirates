namespace Piratas.Servidor.Dominio.Excecoes.Campo
{
    public class CrewMemberNotFoundException : BaseFieldException
    {
        public CrewMemberNotFoundException()
            : base("crew-member-not-found", "Crew member not found.")
        {
        }
    }
}

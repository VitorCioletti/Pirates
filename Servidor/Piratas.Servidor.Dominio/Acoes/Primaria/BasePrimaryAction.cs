namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    public abstract class BasePrimaryAction : BaseAction
    {
        protected BasePrimaryAction(Player starter, Player target = null) : base(starter, target)
        {
        }
    }
}

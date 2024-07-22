namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    public abstract class BaseImmediateAction : BaseAction
    {
        protected BaseImmediateAction(Player starter, Player target = null) : base(starter, target)
        {
        }
    }
}

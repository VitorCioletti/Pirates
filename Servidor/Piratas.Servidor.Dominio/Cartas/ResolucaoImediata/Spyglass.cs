namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;

    public class Spyglass : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            List<string> targetCards = action.Target.Hand.GetAll().Select(c => c.Id.ToString()).ToList();

            var discardCard = new DiscardCard(
                action,
                action.Starter,
                action.Target,
                targetCards);

            return new List<BaseAction> {discardCard};
        }
    }
}

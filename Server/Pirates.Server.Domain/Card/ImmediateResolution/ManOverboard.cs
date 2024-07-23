namespace Pirates.Server.Domain.Card.ImmediateResolution
{
    using System.Collections.Generic;
    using Action;
    using Action.Resultant;

    public class ManOverboard : BaseImmediateResolution
    {
        public override List<BaseAction> ApplyEffect(BaseAction action, Table table)
        {
            Player alvo = action.Target;

            var afogarTripulante = new DrownCrewMember(action, action.Starter, alvo);
            var acoesResultantes = new List<BaseAction> { afogarTripulante };

            return acoesResultantes;
        }
    }
}

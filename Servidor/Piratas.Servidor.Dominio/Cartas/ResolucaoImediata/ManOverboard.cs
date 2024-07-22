namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;

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

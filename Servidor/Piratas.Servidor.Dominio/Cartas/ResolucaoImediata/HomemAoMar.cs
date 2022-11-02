namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;

    public class HomemAoMar : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Jogador alvo = baseAcao.Alvo;

            var afogarTripulante = new AfogarTripulante(baseAcao, baseAcao.Realizador, alvo);
            var acoesResultantes = new List<BaseAcao> { afogarTripulante };

            return acoesResultantes;
        }
    }
}

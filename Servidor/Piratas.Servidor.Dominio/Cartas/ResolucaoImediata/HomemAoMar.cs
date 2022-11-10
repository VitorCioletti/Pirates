namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;

    public class HomemAoMar : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Jogador alvo = acao.Alvo;

            var afogarTripulante = new AfogarTripulante(acao, acao.Realizador, alvo);
            var acoesResultantes = new List<BaseAcao> { afogarTripulante };

            return acoesResultantes;
        }
    }
}

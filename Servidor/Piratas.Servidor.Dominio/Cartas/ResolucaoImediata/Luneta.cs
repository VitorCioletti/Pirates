namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;

    public class Luneta : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            List<string> idsCartasMaoAlvo = baseAcao.Alvo.Mao.ObterTodas().Select(c => c.Id.ToString()).ToList();

            var descartarCarta = new DescartarCarta(baseAcao, baseAcao.Realizador, baseAcao.Alvo, idsCartasMaoAlvo);
            var acoesResultantes = new List<BaseAcao> {descartarCarta};

            return acoesResultantes;
        }
    }
}

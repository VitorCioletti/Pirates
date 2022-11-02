namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class ServoPoseidon : BaseEmbarcacao
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            PilhaDescarte pilhaDescarte = mesa.PilhaDescarte;

            var escolherCartaBaralho = new EscolherCartaBaralho(
                baseAcao,
                baseAcao.Realizador,
                pilhaDescarte,
                pilhaDescarte.ObterTodas<Carta>());
            var acoesResultantes = new List<BaseAcao> { escolherCartaBaralho };

            return acoesResultantes;
        }
    }
}

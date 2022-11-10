namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;

    public class Mergulhador : BaseResolucaoImediata
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            PilhaDescarte pilhaDescarte = mesa.PilhaDescarte;

            var escolherCartaBaralho = new EscolherCartaBaralho(
                acao,
                acao.Realizador,
                pilhaDescarte,
                pilhaDescarte.ObterTodas<Carta>());

            var acoesResultantes = new List<BaseAcao> { escolherCartaBaralho };

            return acoesResultantes;
        }
    }
}

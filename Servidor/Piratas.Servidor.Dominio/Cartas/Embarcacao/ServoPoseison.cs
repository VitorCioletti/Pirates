namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;
    using Tipos;

    public class ServoPoseidon : Embarcacao
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            PilhaDescarte pilhaDescarte = mesa.PilhaDescarte;

            var escolherCartaBaralho = new EscolherCartaBaralho(
                acao,
                acao.Realizador,
                pilhaDescarte,
                pilhaDescarte.ObterTodas<Carta>());
            var acoesResultantes = new List<Acao> { escolherCartaBaralho };

            return acoesResultantes;
        }
    }
}

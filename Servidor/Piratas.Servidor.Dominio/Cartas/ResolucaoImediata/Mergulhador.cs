namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;
    using Tipos;

    public class Mergulhador : ResolucaoImediata
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
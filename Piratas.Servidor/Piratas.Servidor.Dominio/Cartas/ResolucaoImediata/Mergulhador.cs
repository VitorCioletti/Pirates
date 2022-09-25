namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Baralhos.Tipos;
    using Tipos;

    public class Mergulhador : ResolucaoImediata
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            yield return
                new EscolherCartaBaralho(acao, acao.Realizador, pilhaDescarte, pilhaDescarte.ObterTodas<Carta>());
        }
    }
}

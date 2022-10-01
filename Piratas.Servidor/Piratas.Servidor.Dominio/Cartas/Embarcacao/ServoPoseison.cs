namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class ServoPoseidon : Embarcacao
    {
        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            yield return
                new EscolherCartaBaralho(acao, acao.Realizador, pilhaDescarte, pilhaDescarte.ObterTodas<Carta>());
        }
    }
}

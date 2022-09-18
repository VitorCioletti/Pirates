namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;
    using Excecoes.Cartas;
    using System.Collections.Generic;
    using System.Linq;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            var realizador = acao.Realizador;

            if (realizador.Campo.TripulacaoCheia())
                throw new TripulacaoCheiaException(this, realizador);

            var tripulantesDescartados = pilhaDescarte.ObterTodas<Tripulante>().OfType<Carta>().ToList();

            if (tripulantesDescartados.Count == 0)
                throw new SemTripulacaoPilhaDescarteException(this);

            yield return new EscolherCartaBaralho(acao, realizador, pilhaDescarte, tripulantesDescartados);
        }
    }
}

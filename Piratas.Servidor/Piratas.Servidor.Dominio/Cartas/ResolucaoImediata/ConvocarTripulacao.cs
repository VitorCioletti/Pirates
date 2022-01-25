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
        public ConvocarTripulacao(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.PilhaDescarte);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, PilhaDescarte pilhaDescarte)
        {
            var realizador = acao.Realizador;

            if (realizador.Campo.TripulacaoCheia())
                throw new TripulacaoCheiaException(this, realizador);

            var tripulacoesDescartadas = pilhaDescarte.ObterTodas<Tripulacao>().OfType<Carta>().ToList();

            if (tripulacoesDescartadas.Count == 0)
                throw new SemTripulacaoPilhaDescarteException(this);

            yield return new EscolherCartaBaralho(acao, realizador, pilhaDescarte, tripulacoesDescartadas);
        }
    }
}

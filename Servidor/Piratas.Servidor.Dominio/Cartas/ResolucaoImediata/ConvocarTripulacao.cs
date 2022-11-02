namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos;
    using Excecoes.Cartas;
    using Tripulacao;

    public class ConvocarTripulacao : BaseResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            PilhaDescarte pilhaDescarte = mesa.PilhaDescarte;
            Jogador realizador = acao.Realizador;

            if (realizador.Campo.TripulacaoCheia())
                throw new TripulacaoCheiaExcecao(this, realizador);

            List<Carta> tripulantesDescartados = pilhaDescarte.ObterTodas<BaseTripulante>().OfType<Carta>().ToList();

            if (tripulantesDescartados.Count == 0)
                throw new SemTripulacaoPilhaDescarteExcecao(this);

            var escolherCartaBaralho = new EscolherCartaBaralho(
                acao,
                realizador,
                pilhaDescarte,
                tripulantesDescartados);

            var acoesResultantes = new List<Acao> { escolherCartaBaralho };

            return acoesResultantes;
        }
    }
}

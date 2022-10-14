namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using System.Linq;
    using Acoes;
    using Acoes.Resultante;
    using Baralhos.Tipos;
    using Excecoes.Cartas;
    using Tipos;

    public class ConvocarTripulacao : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            PilhaDescarte pilhaDescarte = mesa.PilhaDescarte;
            Jogador realizador = acao.Realizador;

            if (realizador.Campo.TripulacaoCheia())
                throw new TripulacaoCheiaException(this, realizador);

            var tripulantesDescartados = pilhaDescarte.ObterTodas<Tripulante>().OfType<Carta>().ToList();

            if (tripulantesDescartados.Count == 0)
                throw new SemTripulacaoPilhaDescarteException(this);

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

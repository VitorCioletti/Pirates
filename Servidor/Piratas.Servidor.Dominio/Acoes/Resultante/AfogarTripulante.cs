namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Cartas.ResolucaoImediata;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Enums;
    using Excecoes.Acoes;
    using Primaria;

    public class AfogarTripulante : BaseResultanteComListaEscolhas
    {
        public AfogarTripulante(
            Acao origem,
            Jogador realizador,
            Jogador alvo)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                alvo.Mao.ObterTodas<Tripulante>().Select(t => t.Id).ToList(),
                alvo: alvo)
        {
            var tripulacao = alvo.Campo.Tripulacao;

            if (tripulacao.Count == 0)
                throw new NaoPossuiTripulacaoExcecao(this, alvo.Id);

            if (tripulacao.All(t => !t.Afogavel))
                throw new NenhumTripulantePodeSerAfogadoExcecao(this, alvo.Id);
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            var tripulanteEscolhido = (Tripulante)Alvo.Mao.ObterPorId(escolha);

            if (Origem is DescerCarta descerCarta)
            {
                if (descerCarta.Carta is HomemAoMar homemAoMar)
                {
                    if (tripulanteEscolhido is PirataNobre)
                    {
                        throw new TripulanteNaoPodeSerAfogadoExcecao(this, tripulanteEscolhido.Id, homemAoMar.Id);
                    }
                }
            }

            if (!tripulanteEscolhido.Afogavel)
                throw new TripulanteNaoPodeSerAfogadoExcecao(this, tripulanteEscolhido.Id);

            Alvo.Campo.Remover(tripulanteEscolhido);
            mesa.PilhaDescarte.InserirTopo(tripulanteEscolhido);

            return null;
        }
    }
}

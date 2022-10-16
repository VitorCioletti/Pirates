namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Cartas.ResolucaoImediata;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Dominio.Excecoes.Acoes;
    using Primaria;
    using Tipos;

    public class AfogarTripulante : Resultante
    {
        public Tripulante TripulanteAfogado { get; private set; }

        public AfogarTripulante(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
            var tripulacao = alvo.Campo.Tripulacao;

            if (tripulacao.Count == 0)
                throw new NaoPossuiTripulacaoExcecao(this, alvo.Id);

            if (tripulacao.All(t => !t.Afogavel))
                throw new NenhumTripulantePodeSerAfogadoExcecao(this, alvo.Id);
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            if (Origem is DescerCarta descerCarta)
            {
                if (descerCarta.Carta is HomemAoMar homemAoMar)
                {
                    if (TripulanteAfogado is PirataNobre)
                    {
                        throw new TripulanteNaoPodeSerAfogadoExcecao(this, TripulanteAfogado.Id, homemAoMar.Id);
                    }
                }
            }

            if (!TripulanteAfogado.Afogavel)
                throw new TripulanteNaoPodeSerAfogadoExcecao(this, TripulanteAfogado.Id);

            Alvo.Campo.Remover(TripulanteAfogado);
            mesa.PilhaDescarte.InserirTopo(TripulanteAfogado);

            return null;
        }

        public override void PreencherCartaEscolhida(string idCartaEscolhida)
        {
            Tripulante tripulacaoEscolhida = Alvo.Campo.Tripulacao.First(c => c.Id == idCartaEscolhida);

            TripulanteAfogado = tripulacaoEscolhida;
        }
    }
}

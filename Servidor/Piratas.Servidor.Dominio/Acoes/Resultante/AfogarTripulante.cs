namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Cartas.ResolucaoImediata;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Excecoes;
    using Primaria;
    using Tipos;

    public class AfogarTripulante : Resultante
    {
        public Tripulante TripulanteAfogado { get; private set; }

        public AfogarTripulante(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
            var tripulacao = alvo.Campo.Tripulacao;

            if (tripulacao.Count == 0)
                throw new NaoPossuiTripulacaoException(alvo.Id);

            if (tripulacao.All(t => !t.Afogavel))
                throw new NenhumTripulantePodeSerAfogadoException(alvo.Id);
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            if (Origem is DescerCarta descerCarta)
            {
                if (descerCarta.Carta is HomemAoMar homemAoMar)
                {
                    if (TripulanteAfogado is PirataNobre)
                    {
                        throw new TripulanteNaoPodeSerAfogadoException(TripulanteAfogado.Id, homemAoMar.Id);
                    }
                }
            }

            if (!TripulanteAfogado.Afogavel)
                throw new TripulanteNaoPodeSerAfogadoException(TripulanteAfogado.Id);

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

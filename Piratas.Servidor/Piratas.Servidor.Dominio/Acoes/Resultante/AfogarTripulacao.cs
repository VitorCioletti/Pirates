namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Acoes.Primaria;
    using Cartas.ResolucaoImediata;
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Dominio;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Tipos;

    public class AfogarTripulacao : Resultante
    {
        public Tripulacao TripulacaoAfogada { get; private set; }

        public AfogarTripulacao(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
            var tripulacao = realizador.Campo.Tripulacao;

            if (tripulacao.Count == 0)
                throw new Exception($"Jogador \"{realizador}\" não possui tripulação.");

            if (tripulacao.All(t => !t.Afogavel))
                throw new Exception($"Nenhuma tripulação de \"{realizador}\" pode ser afogada.");
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (Origem is DescerCarta descerCarta)
            {
                if (descerCarta.Carta is HomemAoMar)
                {
                    if (TripulacaoAfogada is PirataNobre)
                    {
                        throw new Exception(
                            $"\"{nameof(PirataNobre)}\" não pode ser afogada por \"{nameof(HomemAoMar)}\".");
                    }
                }
            }

            if (!TripulacaoAfogada.Afogavel)
                throw new Exception($"Essa tripulação não pode ser afogada.");

            Alvo.Campo.Remover(TripulacaoAfogada);
            mesa.PilhaDescarte.InserirTopo(TripulacaoAfogada);

            yield return null;
        }
    }
}

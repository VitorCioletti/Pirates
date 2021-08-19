namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Cartas.Tipos;
    using Regras;
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
            if (!TripulacaoAfogada.Afogavel)
                throw new Exception($"Essa tripulação não pode ser afogada.");

            Alvo.Campo.Remover(TripulacaoAfogada);
            mesa.PilhaDescarte.InserirTopo(TripulacaoAfogada);

            yield return null;
        }
    }
}
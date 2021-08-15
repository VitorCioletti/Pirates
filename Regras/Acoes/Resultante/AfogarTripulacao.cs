namespace ServidorPiratas.Regras.Acoes.Resultante
{
    using Cartas.Tipos;
    using Regras;
    using System;
    using Tipos;

    public class AfogarTripulacao : Resultante
    {
        public Tripulacao TripulacaoAfogada { get; private set; }

        public AfogarTripulacao(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (!TripulacaoAfogada.Afogavel)
                throw new Exception($"Essa tripulação não pode ser afogada.");

            Alvo.Campo.Remover(TripulacaoAfogada);
            mesa.PilhaDescarte.InserirTopo(TripulacaoAfogada);

            return null;
        }
    }
}
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Cartas.Tripulacao;
    using Regras;
    using System;
    using Tipos;

    public class AfogarTripulacao : Resultante
    {
        public Tripulacao TripulacaoAfogada { get; private set; }

        public AfogarTripulacao(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (!TripulacaoAfogada.PermiteAfogamento)
                throw new Exception($"Essa tripulação não pode ser afogada.");

            Alvo.Campo.Remover(TripulacaoAfogada);
            mesa.PilhaDescarte.InserirTopo(TripulacaoAfogada);

            return null;
        }
    }
}
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class AfogarTripulacao : Resultante
    {
        public Tripulacao TripulacaoAfogada { get; private set; }

        public AfogarTripulacao(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            Alvo.Campo.Remover(TripulacaoAfogada);
            mesa.PilhaDescarte.InserirTopo(TripulacaoAfogada);

            return null;
        }
    }
}
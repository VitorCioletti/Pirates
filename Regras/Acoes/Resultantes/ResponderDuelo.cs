namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class ResponderDuelo : Resultante
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public Duelo CartaResposta { get; private set; }

        public ResponderDuelo(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            CartaResposta?.AplicarEfeito(this, mesa);

            Vitorioso = Realizador.Campo.CalcularPontosDuelo() > Alvo.Campo.CalcularPontosDuelo() ? Realizador : Alvo;
            Perdedor = Vitorioso == Realizador ? Realizador : Alvo;

            Perdedor.Campo.AfogarTripulacao();
            Perdedor.Campo.DanificarEmbarcacao();

            mesa.SairModoDuelo();

            return new RoubarCarta(Vitorioso, Perdedor);
        }
    }
}
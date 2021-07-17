namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class ResponderDuelo : Resultante
    {
        public Jogador Vitorioso { get; private set; }

        public Duelo Canhao { get; private set; }

        public ResponderDuelo(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (Canhao != null)
                Realizador.Canhao = Canhao;

            Vitorioso = Realizador.CalcularPontosDuelo() > Alvo.CalcularPontosDuelo() ? Realizador : Alvo;

            var perdedor = Vitorioso == Realizador ? Realizador : Alvo;

            Alvo.Tripulacao.Clear();
            Realizador.Tripulacao.Clear();

            mesa.EmDuelo = false;
            mesa.Duelistas = null;

            return new RoubarCarta(Vitorioso, perdedor);
        }
    }
}
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class ResponderDuelo : Resultante
    {
        public Jogador Vitorioso {get; private set; }

        public Duelo Canhao { get; private set; }

        public ResponderDuelo(Jogador realizador, Jogador alvo, Duelo canhao) : base(realizador, alvo) =>
            Canhao = canhao;

        public override void AplicaRegra(Mesa mesa)
        {
            if (Canhao != null)
                Realizador.Canhao = Canhao;

            Vitorioso = Realizador.CalculaPontosDuelo() > Alvo.CalculaPontosDuelo() ? Realizador : Alvo;

            Alvo.Tripulacao.Clear();
            Realizador.Tripulacao.Clear();

            mesa.EmDuelo = false;
            mesa.Duelistas = null;
        }
    }
}
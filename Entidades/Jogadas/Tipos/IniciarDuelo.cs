namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    public class IniciarDuelo: Jogada
    {
        public Jogador Vitorioso { get; private set; }

        public IniciarDuelo(Jogador atacante, Jogador alvo) : base(atacante, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            Vitorioso = Realizador.CalculaPontosDuelo() > Alvo.CalculaPontosDuelo() ? Realizador : Alvo;

            Alvo.Tripulacao.Clear();
            Realizador.Tripulacao.Clear();
        }
    }
}
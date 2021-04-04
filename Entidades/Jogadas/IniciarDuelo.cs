namespace ServidorPiratas.Entidades.Jogadas
{
    public class IniciarDuelo: Jogada
    {
        public Jogador Defensor { get; private set; }

        public IniciarDuelo(Jogador atacante, Jogador defensor) : base(atacante)
        {
            Defensor = defensor;
        }

        public override void AplicaRegra(Mesa mesa)
        {

        }

        private int _calculaPontosDuelo(Jogador jogador) => 3;
    }
}
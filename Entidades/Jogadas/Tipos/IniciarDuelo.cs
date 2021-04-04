namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    public class IniciarDuelo: Jogada
    {
        public Jogador Defensor { get; private set; }

        public IniciarDuelo(Jogador atacante, Jogador defensor) : base(atacante)
        {
            Defensor = defensor;
        }

        public override void AplicarRegra(Mesa mesa)
        {

        }

        private int _calculaPontosDuelo(Jogador jogador) => 3;
    }
}
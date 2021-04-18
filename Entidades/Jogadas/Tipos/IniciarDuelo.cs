namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    public class IniciarDuelo: Jogada
    {
        public IniciarDuelo(Jogador atacante, Jogador alvo) : base(atacante, alvo) {}

        public override void AplicarRegra(Mesa mesa)
        {

        }

        private int _calculaPontosDuelo(Jogador jogador) => 3;
    }
}
namespace ServidorPiratas.Entidades.Jogadas.Tipos
{
    using Cartas;

    public class Duelar: Jogada
    {
        public Jogador Vitorioso { get; private set; }

        public Duelar(Jogador atacante, Jogador alvo) : base(atacante, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            Vitorioso = Realizador.CalculaPontosDuelo() > Alvo.CalculaPontosDuelo() ? Realizador : Alvo;

            Alvo.Tripulacao.Clear();
            Realizador.Tripulacao.Clear();
        }
    }
}
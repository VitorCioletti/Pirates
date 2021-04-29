namespace ServidorPiratas.Entidades.Acoes.Tipos
{
    using Cartas;

    public class Duelar: Acao
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
namespace ServidorPiratas.Regras.Acoes.Tipos.Jogador
{
    using Regras;

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
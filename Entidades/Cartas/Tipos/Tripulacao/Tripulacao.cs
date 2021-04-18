using ServidorPiratas.Entidades.Jogadas;

namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    public abstract class Tripulacao : Carta
    {
        public Tripulacao(string nome) : base(nome) { }

        public int Tiros { get; protected set; }

        public override void AplicaEfeito(Jogada _, Mesa __) {}
    }
}
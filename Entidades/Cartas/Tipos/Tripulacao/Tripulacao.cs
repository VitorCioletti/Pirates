using ServidorPiratas.Entidades.Acoes;

namespace ServidorPiratas.Entidades.Cartas.Tipos.Tripulacao
{
    public abstract class Tripulacao : Carta
    {
        public Tripulacao(string nome) : base(nome) { }

        public int Tiros { get; protected set; }

        public override void AplicaEfeito(Acao _, Mesa __) {}
    }
}
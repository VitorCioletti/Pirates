
namespace ServidorPiratas.Regras.Cartas.Tipos
{
    using Acoes;

    public abstract class Embarcacao : Carta
    {
        public Embarcacao(string nome) : base(nome) { }

        public override void AplicaEfeito(Acao Acao, Mesa mesa)
        {
            throw new System.NotImplementedException();
        }
    }
}
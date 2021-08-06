
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class EfeitoEmbarcacao : Resultante
    {
        public Embarcacao Embarcacao { get; private set; }
        
        public EfeitoEmbarcacao(Jogador realizador, Embarcacao embarcacao) : base(realizador) =>
            Embarcacao = embarcacao;

        public override Resultante AplicarRegra(Mesa mesa) => Embarcacao?.AplicarEfeito(this, mesa);
    }
}
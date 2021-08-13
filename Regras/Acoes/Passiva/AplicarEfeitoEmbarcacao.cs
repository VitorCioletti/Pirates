
namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using System;
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class AplicarEfeitoEmbarcacao : Passiva
    {
        public Embarcacao Embarcacao { get; private set; }
        
        public AplicarEfeitoEmbarcacao(Jogador realizador, Embarcacao embarcacao) : base(realizador) =>
            Embarcacao = embarcacao;

        public override Resultante AplicarRegra(Mesa mesa) 
        {
            if (Embarcacao == null)
                throw new ArgumentNullException(nameof(Embarcacao));

            return Embarcacao.AplicarEfeito(this, mesa);
        }
    }
}

namespace Piratas.Servidor.Regras.Acoes.Passiva
{
    using Acoes.Tipos;
    using Cartas.Tipos;
    using Regras;
    using System;

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
namespace Piratas.Servidor.Dominio.Acoes.Passiva
{
    using System;
    using System.Collections.Generic;
    using Cartas.Tipos;
    using Tipos;

    public class AplicarEfeitoEmbarcacao : Passiva
    {
        public Embarcacao Embarcacao { get; private set; }

        public AplicarEfeitoEmbarcacao(Jogador realizador, Embarcacao embarcacao) : base(realizador) =>
            Embarcacao = embarcacao;

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            if (Embarcacao == null)
                throw new ArgumentNullException(nameof(Embarcacao));

            return Embarcacao.AplicarEfeito(this, mesa);
        }
    }
}

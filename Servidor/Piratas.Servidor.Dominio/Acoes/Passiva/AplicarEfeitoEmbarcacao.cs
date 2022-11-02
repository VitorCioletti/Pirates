namespace Piratas.Servidor.Dominio.Acoes.Passiva
{
    using System.Collections.Generic;
    using Cartas.Embarcacao;

    public class AplicarEfeitoEmbarcacao : BasePassiva
    {
        private BaseEmbarcacao BaseEmbarcacao { get; set; }

        public AplicarEfeitoEmbarcacao(Jogador realizador, BaseEmbarcacao baseEmbarcacao) : base(realizador) =>
            BaseEmbarcacao = baseEmbarcacao;

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            return BaseEmbarcacao.AplicarEfeito(this, mesa);
        }
    }
}

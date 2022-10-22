namespace Piratas.Servidor.Dominio.Acoes.Passiva
{
    using System.Collections.Generic;
    using Cartas.Tipos;
    using Resultante.Base;

    public class AplicarEfeitoEmbarcacao : Passiva
    {
        private Embarcacao _embarcacao { get; set; }

        public AplicarEfeitoEmbarcacao(Jogador realizador, Embarcacao embarcacao) : base(realizador) =>
            _embarcacao = embarcacao;

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            return _embarcacao.AplicarEfeito(this, mesa);
        }
    }
}

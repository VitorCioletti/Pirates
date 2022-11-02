namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class GuerrilhaNaval : BaseEmbarcacao
    {
        public int TirosAdicionais { get; private set; } = 2;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}

namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class GuerrilhaNaval : BaseEmbarcacao
    {
        public int TirosAdicionais { get; private set; } = 2;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa) => null;
    }
}

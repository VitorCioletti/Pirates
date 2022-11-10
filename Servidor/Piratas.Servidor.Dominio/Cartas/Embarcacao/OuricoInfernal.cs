namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class OuricoInfernal : BaseEmbarcacao
    {
        public int Tiros { get; private set; } = 3;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa) => null;
    }
}

namespace Piratas.Servidor.Dominio.Cartas.Passivo
{
    using System.Collections.Generic;
    using Acoes;

    public class BauArmadilha : BasePassivo
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa) => null;
    }
}

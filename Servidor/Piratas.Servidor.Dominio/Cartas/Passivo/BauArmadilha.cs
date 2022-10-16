namespace Piratas.Servidor.Dominio.Cartas.Passivo
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class BauArmadilha : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}

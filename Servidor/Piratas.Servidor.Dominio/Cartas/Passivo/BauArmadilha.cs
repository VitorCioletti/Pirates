namespace Piratas.Servidor.Dominio.Cartas.Passivo
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class BauArmadilha : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}

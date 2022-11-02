namespace Piratas.Servidor.Dominio.Cartas.Passivo
{
    using System.Collections.Generic;
    using Acoes;

    public class BauArmadilha : BasePassivo
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}

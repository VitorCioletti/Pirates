namespace Piratas.Servidor.Dominio.Cartas.Passivo
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class BauArmadilha : ResolucaoImediata
    {
        public BauArmadilha(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}

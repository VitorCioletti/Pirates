namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Resultante;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            var descartarCarta = new DescartarCarta(acao, acao.Realizador, acao.Alvo);
            var acoesResultantes = new List<Acao> {descartarCarta};

            return acoesResultantes;
        }
    }
}

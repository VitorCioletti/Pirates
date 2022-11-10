namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Imediata;
    using Acoes.Primaria;

    public class MercadorTortuga : BaseEmbarcacao
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            var comprarCarta = new ComprarCarta(acao.Realizador);

            var copiarPrimaria = new CopiarPrimaria(acao.Realizador, comprarCarta);
            var acoesResultantes = new List<BaseAcao> {copiarPrimaria};

            return acoesResultantes;
        }
    }
}

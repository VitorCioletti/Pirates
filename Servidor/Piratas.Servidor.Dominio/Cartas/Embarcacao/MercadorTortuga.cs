namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Tipos;

    public class MercadorTortuga : BaseEmbarcacao
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            var comprarCarta = new ComprarCarta(baseAcao.Realizador);

            var copiarPrimaria = new CopiarPrimaria(baseAcao.Realizador, comprarCarta);
            var acoesResultantes = new List<BaseAcao> {copiarPrimaria};

            return acoesResultantes;
        }
    }
}

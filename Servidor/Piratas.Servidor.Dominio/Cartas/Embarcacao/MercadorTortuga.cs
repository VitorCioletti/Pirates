namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Primaria;
    using Acoes.Resultante;
    using Tipos;

    public class MercadorTortuga : Embarcacao
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            var comprarCarta = new ComprarCarta(acao.Realizador);

            var copiarPrimaria = new CopiarPrimaria(acao, acao.Realizador, comprarCarta);
            var acoesResultantes = new List<Acao> { copiarPrimaria };

            return acoesResultantes;
        }
    }
}

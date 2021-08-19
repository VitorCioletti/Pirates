namespace Piratas.Servidor.Regras.Cartas.ResolucaoImediata
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;
    using Baralhos.Tipos;
    using Tipos;

    public class Rum : ResolucaoImediata
    {
        public Rum(string nome) : base(nome) { }

        private int _cartasCompradas = 2;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador.Mao, mesa.BaralhoCentral);

        internal IEnumerable<Resultante> _aplicarEfeito(Mao maoRealizador, BaralhoCentral baralhoCentral)
        {
            var cartasCompradas = baralhoCentral.ObterTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);

            yield return null;
        }
    }
}
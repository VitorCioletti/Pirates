namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using Acoes.Tipos;
    using System;
    using Tipos;

    public class Rum : ResolucaoImediata
    {
        public Rum(string nome) : base(nome) { }

        private int _cartasCompradas = 2;

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador.Mao, mesa.BaralhoCentral);

        internal Resultante _aplicaEfeito(Mao maoRealizador, BaralhoCentral baralhoCentral)
        {
            var cartasCompradas = baralhoCentral.ObterTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);

            return null;
        }
    }
}
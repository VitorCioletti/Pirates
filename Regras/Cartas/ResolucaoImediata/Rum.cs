namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using System;
    using Tipos;

    public class Rum : ResolucaoImediata
    {
        public Rum(string nome) : base(nome) { }

        private int _cartasCompradas = 2;

        public override void AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador.Mao, mesa.BaralhoCentral);

        internal void _aplicaEfeito(Mao maoRealizador, BaralhoCentral baralhoCentral)
        {
            var cartasCompradas = baralhoCentral.ObterTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);
        }

        private int _calculaCartaSaqueada(int quantidadeCartas) => new Random().Next(0, quantidadeCartas);
    }
}
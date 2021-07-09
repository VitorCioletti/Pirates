namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using Baralhos;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Rum : ResolucaoImediata
    {
        public Rum(string nome) : base(nome) { }

        private int _cartasCompradas = 2;

        public void AplicaEfeito(List<Carta> maoRealizador, List<Carta> maoAlvo) { }

        public override void AplicaEfeito(Acao acao, Mesa mesa) => 
            _aplicaEfeito(acao.Realizador.Mao, mesa.BaralhoCentral);

        internal void _aplicaEfeito(Mao maoRealizador, Central baralhoCentral)
        {
            var cartasCompradas = baralhoCentral.ObtemTopo(_cartasCompradas);
            maoRealizador.Adicionar(cartasCompradas);
        }

        private int _calculaCartaSaqueada(int quantidadeCartas) => new Random().Next(0, quantidadeCartas);
    }
}
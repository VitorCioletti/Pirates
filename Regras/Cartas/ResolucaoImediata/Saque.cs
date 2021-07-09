namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public void AplicaEfeito(List<Carta> maoRealizador, List<Carta> maoAlvo) { }

        public override void AplicaEfeito(Acao acao, Mesa _) => 
            _aplicaEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal void _aplicaEfeito(List<Carta> maoRealizador, List<Carta> maoAlvo)
        {
            var posicaoCartaSaqueada = _calculaCartaSaqueada(maoAlvo.Count);
            var cartaSaqueada = maoAlvo[posicaoCartaSaqueada];

            maoAlvo.RemoveAt(posicaoCartaSaqueada);
            maoRealizador.Add(cartaSaqueada);
        }

        private int _calculaCartaSaqueada(int quantidadeCartas) => new Random().Next(0, quantidadeCartas);
    }
}
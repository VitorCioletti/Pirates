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

        public override void AplicarEfeito(Acao acao, Mesa _) => 
            _aplicaEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal void _aplicaEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            var cartaSaqueada = maoAlvo.ObterQualquer();

            maoAlvo.Remover(cartaSaqueada);
            maoRealizador.Adicionar(cartaSaqueada);
        }
    }
}
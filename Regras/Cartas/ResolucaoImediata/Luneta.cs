namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Luneta : ResolucaoImediata
    {
        public Luneta(string nome) : base(nome) { }

        public void AplicaEfeito(List<Carta> maoRealizador, List<Carta> maoAlvo) { }

        public override void AplicaEfeito(Acao acao, Mesa _) => 
            _aplicaEfeito(null, acao.Alvo.Mao);

        internal void _aplicaEfeito(Carta cartaDescartada, Mao maoAlvo)
        {
            maoAlvo.Remover(cartaDescartada);
        }
    }
}
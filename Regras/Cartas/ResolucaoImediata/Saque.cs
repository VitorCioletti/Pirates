namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa _) => 
            _aplicaEfeito(acao.Realizador.Mao, acao.Alvo.Mao);

        internal Resultante _aplicaEfeito(Mao maoRealizador, Mao maoAlvo)
        {
            var cartaSaqueada = maoAlvo.ObterQualquer();

            maoAlvo.Remover(cartaSaqueada);
            maoRealizador.Adicionar(cartaSaqueada);

            return null;
        }
    }
}
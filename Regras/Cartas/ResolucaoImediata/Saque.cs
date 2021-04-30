namespace ServidorPiratas.Regras.Cartas.ResolucaoImediata
{
    using System;
    using Acoes;
    using Tipos;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public override void AplicaEfeito(Acao Acao, Mesa _)
        {
            var realizador = Acao.Realizador;
            var alvo = Acao.Alvo;

            if (alvo != null)
            {
                var posicaoCartaSaqueada = _calculaCartaSaqeuada(alvo.Mao.Count);
                var cartaSaqueada = alvo.Mao[posicaoCartaSaqueada];

                alvo.Mao.RemoveAt(posicaoCartaSaqueada);
                realizador.Mao.Add(cartaSaqueada);
            }
            else
                throw new Exception("Não é possível aplicar regra da carta sem um alvo.");
        }

        private int _calculaCartaSaqeuada(int quantidadeCartas) => new Random().Next(0, quantidadeCartas);
    }
}
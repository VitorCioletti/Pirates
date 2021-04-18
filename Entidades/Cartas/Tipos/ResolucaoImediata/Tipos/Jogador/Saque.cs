namespace ServidorPiratas.Entidades.Cartas.Tipos.ResolucaoImediata
{
    using System;
    using Jogadas;

    public class Saque : ResolucaoImediata
    {
        public Saque(string nome) : base(nome) { }

        public override void AplicaEfeito(Jogada jogada, Mesa _)
        {
            var realizador = jogada.Realizador;
            var alvo = jogada.Alvo;

            if (alvo != null)
            {
                var posicaoCartaSaqueada = _calculaCartaSaqeuada(alvo.CartasNaMao.Count);
                var cartaSaqueada = alvo.CartasNaMao[posicaoCartaSaqueada];

                alvo.CartasNaMao.RemoveAt(posicaoCartaSaqueada);
                realizador.CartasNaMao.Add(cartaSaqueada);
            }
            else
                throw new Exception("Não é possível aplicar regra da carta sem um alvo.");
        }

        private int _calculaCartaSaqeuada(int quantidadeCartas) => new Random().Next(0, quantidadeCartas);
    }
}
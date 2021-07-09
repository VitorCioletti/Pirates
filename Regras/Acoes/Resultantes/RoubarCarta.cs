namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System;
    using Tipos;

    public class RoubarCarta: Resultante
    {
        public string IdAcaoOrigem { get; private set; }

        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaRoubada);
            Alvo.Mao.Remover(CartaRoubada);
        }
    }
}
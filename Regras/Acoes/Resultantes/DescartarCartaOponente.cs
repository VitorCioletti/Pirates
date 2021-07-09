namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System;
    using Tipos;

    public class DescartarCartaOponente: Resultante
    {
        public Carta CartaDescartada { get; private set; }

        public DescartarCartaOponente(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            Alvo.Mao.Remover(CartaDescartada);
            mesa.PilhaDescarte.InsereTopo(CartaDescartada);
        }
    }
}
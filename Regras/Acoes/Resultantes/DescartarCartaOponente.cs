namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System;
    using Tipos;

    public class DescartarCartaOponente: Resultante
    {
        public string IdAcaoOrigem { get; private set; }

        public Carta CartaDescartada { get; private set; }

        public DescartarCartaOponente(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            if (Alvo.Mao.Remove(CartaDescartada))
                mesa.BaralhoDescarte.InsereTopo(CartaDescartada);
            else
                throw new Exception(
                    $"Carta \"{CartaDescartada.Nome}\" não está na mão do jogador \"{Realizador.Id}\".");
        }
    }
}
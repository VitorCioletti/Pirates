namespace ServidorPiratas.Regras.Acoes.Tipos.Resultante
{
    using System;
    using Regras;
    using Cartas;

    public class DescartarCartaOponente: Acao
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
namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using Cartas.Tipos;
    using Regras.Cartas;
    using Regras;
    using System;
    using Tipos;

    public class DescerCarta : Primaria
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta) : base(jogador) => Carta = carta;

        public override void AplicaRegra(Mesa mesa) 
        {
            if (Carta.GetType() == typeof(Tesouro))
                throw new Exception($"Não é permitido jogar cartas \"{nameof(Tesouro)}\".");

            if (!Realizador.Mao.Contains(Carta))
                throw new Exception($"O jogador \"{Realizador.Id}\" não possui a carta \"{Carta.Nome}\".");

            Realizador.Mao.Remove(Carta);
            mesa.BaralhoDescarte.InsereTopo(Carta);

            Carta.AplicaEfeito(this, mesa);
        }
    }
}
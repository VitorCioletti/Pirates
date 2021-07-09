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

        public override void AplicarRegra(Mesa mesa) 
        {
            if (Carta.GetType() == typeof(Tesouro))
                throw new Exception($"Não é permitido jogar cartas \"{nameof(Tesouro)}\".");

            Realizador.Mao.Remover(Carta);
            mesa.PilhaDescarte.InserirTopo(Carta);

            Carta.AplicarEfeito(this, mesa);
        }
    }
}
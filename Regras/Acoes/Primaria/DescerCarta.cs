namespace Piratas.Servidor.Regras.Acoes.Primaria
{
    using Cartas.Tipos;
    using Regras.Cartas;
    using Regras;
    using System;
    using Tipos;

    public class DescerCarta : Primaria
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta, Jogador alvo = null) : base(jogador, alvo) => Carta = carta;

        public override Resultante AplicarRegra(Mesa mesa) 
        {
            if (Carta is Tesouro || Carta is Passivo)
                throw new Exception($"Não é permitido jogar cartas \"{Carta.GetType()}\".");

            var resultanteEfeitoCarta = Carta.AplicarEfeito(this, mesa);

            Realizador.Mao.Remover(Carta);
            mesa.PilhaDescarte.InserirTopo(Carta);

            return resultanteEfeitoCarta; 
        }
    }
}
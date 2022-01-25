namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using Cartas.Tipos;
    using Dominio.Cartas;
    using Dominio;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class DescerCarta : Primaria
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta, Jogador alvo = null) : base(jogador, alvo) => Carta = carta;

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
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

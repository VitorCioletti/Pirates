namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Resultante.Base;

    public class DescerCarta : Primaria
    {
        public Carta Carta { get; private set; }

        public DescerCarta(Jogador jogador, Carta carta, Jogador alvo = null) : base(jogador, alvo) => Carta = carta;

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            if (Carta is Tesouro || Carta is Passivo)
                throw new ProibidoDescerCartaExcecao(this, Carta);

            var resultanteEfeitoCarta = Carta.AplicarEfeito(this, mesa);

            Realizador.Mao.Remover(Carta);
            mesa.PilhaDescarte.InserirTopo(Carta);

            return resultanteEfeitoCarta;
        }
    }
}
